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
    private readonly IUserAnswerService _userAnswerService;
    public readonly IMapper _mapper;

    public UserAttemptService(
        IRepository<UserAttempt> attemptRepository,
        IAuthService authService,
        IMapper mapper,
        IRepository<Test> testRepository,
        IRepository<Option> optionRepository,
        IRepository<Question> questionRepository,
        IUserAnswerService userAnswerService)
    {
        _attemptRepository = attemptRepository;
        _authService = authService;
        _mapper = mapper;
        _testRepository = testRepository;
        _optionRepository = optionRepository;
        _userAnswerService = userAnswerService;
    }

    public async Task<UserAttemptFromResultDto> FinishTest(UserAttemptFromCreateDto dto)
    {
        var test = await _testRepository.GetByIdAsync(dto.TestId);
        if (test is null)
            throw new TestCustomException(404, "Test mavjud emas ");

        int correctAnswersCount = 0;
        int isCorrectAnswersCount = 0;
        foreach (var response in dto.Responses)
        {
            var myOption = await _optionRepository.GetByIdAsync(response.OptionId);
            if (response is null)
                throw new TestCustomException(404, "bunday variant mavjud emas");
            if (myOption.IsCorrect)
            {
                correctAnswersCount++;
            }
        }
        var userId = _authService.TokenFromUserId();

        var attempt = new UserAttempt
        {
            TestId = dto.TestId,
            RightAnswersCount = correctAnswersCount,
            IsCurrectAnswerCount = isCorrectAnswersCount,
            CompletedAt = DateTime.UtcNow,
            UserId = userId,
            StartedAt = dto.StartedAt,
        };
        var createUserAttempt = await _attemptRepository.CreateAsync(attempt);

        Guid AttemptId = createUserAttempt.Id;

        var createAnswer = await _userAnswerService.CreateAsync(dto, AttemptId);

        return new UserAttemptFromResultDto
        {
            CompletedAt = createUserAttempt.CompletedAt,
            CorrectAnswersCount = correctAnswersCount,
            IsCorrectAnswersCount = isCorrectAnswersCount,
            StartedAt = createUserAttempt.StartedAt,
            TestId = createUserAttempt.TestId,
        };
    }

    public async Task<bool> DeleteAsync(Guid Id)
    {
        var userAttempt = _attemptRepository.GetByIdAsync(Id);
        if (userAttempt is null)
            throw new TestCustomException(404, "bu foydalanuvchi urinishi mavjud emas");

        var deleteUserAttempt = await _attemptRepository.DeleteAsync(Id);
        return true;
    }

    public async Task<IEnumerable<UserAttemptFromResultDto>> GetAllAsync(Guid TestId)
    {
        var userAttempts = _attemptRepository.GetAll().Where(x => x.Id == TestId);
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
