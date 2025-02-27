using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestExecution.Api.Models;
using TestExecution.Service.DTOs.Login;
using TestExecution.Service.DTOs.User;
using TestExecution.Service.Interfaces;

namespace TestExecution.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] LoginDto dto)
        {

            var token = await _authService.AuthenticateAsync(dto);
            return Ok(token);    
        }
        [HttpPost]
        public async Task<IActionResult> RegistrAsync([FromBody] RegistrForCreationDto dto)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Seccess",
                Date = await _userService.RegistrAsync(dto)
            };
            return Ok(response);
        }
    }
}
