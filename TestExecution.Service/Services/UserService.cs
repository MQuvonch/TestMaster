using AutoMapper;
using TestExecution.Data.IRepositories;
using TestExecution.Domain.Entities;
using TestExecution.Service.DTOs.Option;
using TestExecution.Service.DTOs.Question;
using TestExecution.Service.DTOs.Test;
using TestExecution.Service.DTOs.User;
using TestExecution.Service.Exceptions;
using TestExecution.Service.Helpers;
using TestExecution.Service.Interfaces;

namespace TestExecution.Service.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> _repository;
    private readonly IMapper _mapper;

    public UserService(IRepository<User> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<UserForResultDto> RegistrAsync(RegistrForCreationDto dto)
    {
        var users = _repository.GetAll();
        var filtereUser = users.Where(u => u.Email == dto.Email).FirstOrDefault();
        if (filtereUser != null)
        {
            throw new TestCustomException(404, "foydalanuvchi mavjud");
        }

        var newUserDto = _mapper.Map<User>(dto);
        var GeneretedPasshwordHash = PasswordHelper.Hash(dto.PasswordHash);
        newUserDto.PasswordHash = GeneretedPasshwordHash;

        var createUser = await _repository.CreateAsync(newUserDto);

        return _mapper.Map<UserForResultDto>(createUser);
    }

    public async Task<bool> DeleteAsync(Guid Id)
    {
        var user = await _repository.GetByIdAsync(Id);
        if (user == null)
            throw new TestCustomException(404, "User mavjud emas ");

        return await _repository.DeleteAsync(Id); ;
    }

    public async Task<IEnumerable<UserForResultDto>> GetAllAsync()
    {
        var users = _repository.GetAll().Select(u=>new UserForResultDto
        {
            Id = u.Id,
            FirstName = u.FirstName,
            LastName = u.LastName,
            Email = u.Email,
            UserName = u.UserName,
            PasswordHash = u.PasswordHash,
            Tests = u.Tests.Select(t=> new TestForResultDto
            {
                Id=t.Id,
                Title = t.Title,    
                Description = t.Description,
                Duration = t.Duration,
                Questions = t.Questions.Select(q=>new QuestionFromResultDto
                {
                    Id=q.Id,    
                    Text=q.Text,    
                    Options = q.Options.Select(o=> new OptionFromResultDto
                    {
                        QuestionId=o.Id,
                        Text = o.Text,
                        IsCorrect=o.IsCorrect,
                    }).ToList()
                }).ToList() 
            }).ToList()
        });
        return users ?? throw new TestCustomException(404, "User mavjud emas"); ;
    }

    public async Task<UserForResultDto> GetByIdAsync(Guid Id)
    {
        var user = await _repository.GetByIdAsync(Id);
        if (user is null)
            throw new TestCustomException(404, "User topilmadi ");


        return _mapper.Map<UserForResultDto>(user); ;
    }

    public async Task<UserForResultDto> UpdateAsync(Guid Id, UserForUpdateDto dto)
    {
        var user = await _repository.GetByIdAsync(Id);
        if (user == null)
            throw new TestCustomException(404, "User topilmadi ");

        var mapUser = _mapper.Map(dto,user);
        var updateUser = await _repository.UpdateAsync(mapUser);

        return _mapper.Map<UserForResultDto>(updateUser);   
    }
}
