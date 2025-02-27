using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TestExecution.Data.IRepositories;
using TestExecution.Domain.Entities;
using TestExecution.Service.DTOs.Login;
using TestExecution.Service.Exceptions;
using TestExecution.Service.Helpers;
using TestExecution.Service.Interfaces;

namespace TestExecution.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IRepository<User> _userRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthService(IRepository<User> userRepository,
                           IConfiguration configuration,
                           IHttpContextAccessor contextAccessor)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }

        public async Task<LoginForResultDto> AuthenticateAsync(LoginDto loginDto)
        {
            var users = await _userRepository.GetAllAsync();
            var filtereUser = users.Where(x => x.Email == loginDto.Email).FirstOrDefault();

            if (filtereUser != null)
            {
                var hashedPassword = PasswordHelper.Verify(loginDto.Password, filtereUser.PasswordHash);
                if (!hashedPassword)
                    throw new TestCustomException(400, "Palor yoki Login notug'ri ");

                return new LoginForResultDto()
                {
                    Token = GenerateUserToken(filtereUser)
                };

            }

            throw new TestCustomException(400, "Palor yoki Login notug'ri ");
        }

        public Guid TokenFromUserId()
        {
            var user = _contextAccessor.HttpContext?.User;
            if (user is null || !user.Identity.IsAuthenticated)
                throw new TestCustomException(400, "Foydalanuvchi Authentication qilnmagan");

            var idClaim = user.Claims.Where(claim=>claim.Type == "Id").FirstOrDefault()?.Value;

            if (string.IsNullOrEmpty(idClaim))
                throw new TestCustomException(400, "Token ichida Id claim mavjuda emas");
            return Guid.Parse(idClaim); 
        }

        private string GenerateUserToken(User user)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
                var tokenDescription = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("Id",user.Id.ToString()),
                    }),
                    Audience = _configuration["JWT:Audience"],
                    Issuer = _configuration["JWT:Issuer"],
                    IssuedAt = DateTime.UtcNow,
                    Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JWT:Expire"])),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescription);
                var respone = tokenHandler.WriteToken(token);

                return respone;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
