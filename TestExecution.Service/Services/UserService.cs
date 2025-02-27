using AutoMapper;
using TestExecution.Data.IRepositories;
using TestExecution.Domain.Entities;
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
        var users = await _repository.GetAllAsync();
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
        var users = await _repository.GetAllAsync();
        if (users is null)
            throw new TestCustomException(404, "User mavjud emas");
        return _mapper.Map<IEnumerable<UserForResultDto>>(users);
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
