using AutoMapper;
using TestExecution.Data.IRepositories;
using TestExecution.Domain.Entities;
using TestExecution.Service.DTOs.UserAnswer;
using TestExecution.Service.Exceptions;
using TestExecution.Service.Interfaces;

namespace TestExecution.Service.Services;

public class UserAnswerService : IUserAnswerService
{
    private readonly IRepository<UserAnswer> _answerRepository;
    private readonly IRepository<Question> _questionRepository;
    private readonly IRepository<Option> _optionRepository;
    private readonly IRepository<UserAttempt> _attemptRepository;
    private readonly IRepository<Test> _testRepository;
    private readonly IMapper _mapper;

    public UserAnswerService(IRepository<UserAnswer> answerRepository,
                             IRepository<Question> questionRepository,
                             IRepository<Option> optionRepository,
                             IRepository<UserAttempt> attemptRepository,
                             IMapper mapper,
                             IRepository<Test> testRepository)
    {
        _answerRepository = answerRepository;
        _questionRepository = questionRepository;
        _optionRepository = optionRepository;
        _attemptRepository = attemptRepository;
        _mapper = mapper;
        _testRepository = testRepository;
    }


    public async Task<IEnumerable<UserAnswerFromResultDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<UserAnswerFromResultDto> GetByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public async Task<UserAnswerFromResultDto> CreateAsync(UserAnswerFromCreateDto dto)
    {
        var question = await _questionRepository.GetByIdAsync(dto.QuestionId);
        var option = await _questionRepository.GetByIdAsync(dto.OptionId);
        var attempt = await _questionRepository.GetByIdAsync(dto.UserAttemptId);

        if (question is null && option is null && attempt is null)
            throw new TestCustomException(404, "Javobi uchun savol yoki javob yoki urunishlari mavjud emas");

        var answerMap = _mapper.Map<UserAnswer>(dto);
        var createAnswer = await _answerRepository.CreateAsync(answerMap);

        return _mapper.Map<UserAnswerFromResultDto>(createAnswer);
    }

    public Task<UserAnswerFromResultDto> UpdateAsync(Guid Id, UserAnswerFromUpdateDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<string> FinishTest(SubmitTestCreationDto dto)
    {
        throw new NotImplementedException();
    }
}
