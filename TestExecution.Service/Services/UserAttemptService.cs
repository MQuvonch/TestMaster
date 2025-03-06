using AutoMapper;
using TestExecution.Data.IRepositories;
using TestExecution.Domain.Entities;
using TestExecution.Service.DTOs.UserAttempt;
using TestExecution.Service.Exceptions;
using TestExecution.Service.Interfaces;

namespace TestExecution.Service.Services;

public class UserAttemptService : IUserAttemptService
{
    private readonly IRepository<UserAttempt> _attemptRepository;
    public readonly IAuthService _authService;
    private readonly IRepository<Test> _testRepository;
    private readonly IRepository<Option> _optionRepository;
    public readonly IMapper _mapper;

    public UserAttemptService(
        IRepository<UserAttempt> attemptRepository,
        IAuthService authService,
        IMapper mapper,
        IRepository<Test> testRepository,
        IRepository<Option> optionRepository)
    {
        _attemptRepository = attemptRepository;
        _authService = authService;
        _mapper = mapper;
        _testRepository = testRepository;
        _optionRepository = optionRepository;
    }

    public async Task<UserAttemptFromResultDto> FinishTest(UserAttemptFromCreateDto dto)
    {
        var test = _testRepository.GetByIdAsync(dto.TestId);
        if (test is null)
            throw new TestCustomException(404, "Test mavjud emas ");

        int correctAnswersCount = 0;
        foreach (var option in dto.Details)
        {
            var myOpton = await _optionRepository.GetByIdAsync(option.OptionId);
            if (option is null)
                throw new TestCustomException(404, "bunday variant mavjud emas");
            if (myOpton.IsCorrect)
                correctAnswersCount++;
        }

        var mapUserAttempt = _mapper.Map<UserAttempt>(dto);
        mapUserAttempt.UserId = _authService.TokenFromUserId();
        mapUserAttempt.RightAnswersCount = correctAnswersCount;

        var createUserAttempt = await _attemptRepository.CreateAsync(mapUserAttempt);
        return _mapper.Map<UserAttemptFromResultDto>(createUserAttempt);
    }

    public async Task<bool> DeleteAsync(Guid Id)
    {
        var userAttempt = _attemptRepository.GetByIdAsync(Id);
        if (userAttempt is null)
            throw new TestCustomException(404, "bu foydalanuvchi urinishi mavjud emas");

        var deleteUserAttempt = await _attemptRepository.DeleteAsync(Id);
        return true;
    }

    public async Task<IEnumerable<UserAttemptFromResultDto>> GetAllAsync()
    {
        var userAttempts = _attemptRepository.GetAll();
        if (userAttempts is null)
            throw new TestCustomException(404, "urunishlar mavjud emas ");

        return _mapper.Map<IEnumerable<UserAttemptFromResultDto>>(userAttempts);
    }

    public async Task<UserAttemptFromResultDto> GetByIdAsync(Guid Id)
    {
        var userAttempt = await _attemptRepository.GetByIdAsync(Id);
        if (userAttempt is null)
            throw new TestCustomException(404, "bu foydalanuvchi urinishi mavjud emas");

        return _mapper.Map<UserAttemptFromResultDto>(userAttempt);
    }

    public async Task<UserAttemptFromResultDto> UpdateAsync(Guid Id, UserAttemptFromUpdateDto dto)
    {
        var userAttempt = await _attemptRepository.GetByIdAsync(Id);
        if (userAttempt is null)
            throw new TestCustomException(404, "bu foydalanuvchi urinishi mavjud emas");

        var userAttemptMap = _mapper.Map(dto, userAttempt);

        var updateUserAttempt = await _attemptRepository.UpdateAsync(userAttemptMap);
        return _mapper.Map<UserAttemptFromResultDto>(userAttemptMap);

    }

}
