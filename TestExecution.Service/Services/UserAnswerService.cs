using AutoMapper;
using TestExecution.Data.Contexts;
using TestExecution.Data.IRepositories;
using TestExecution.Domain.Entities;
using TestExecution.Service.DTOs.UserAnswer;
using TestExecution.Service.DTOs.UserAttempt;
using TestExecution.Service.Exceptions;
using TestExecution.Service.Interfaces;

namespace TestExecution.Service.Services;

public class UserAnswerService : IUserAnswerService
{
    private readonly IRepository<UserAnswer> _answerRepository;
    private readonly IRepository<Question> _questionRepository;
    private readonly IRepository<Option> _optionRepository;
    private readonly IRepository<UserAttempt> _attemptRepository;
    private readonly AppDbContext _context;
    private readonly IRepository<Test> _testRepository;
    private readonly IMapper _mapper;

    public UserAnswerService(IRepository<UserAnswer> answerRepository,
                             IRepository<Question> questionRepository,
                             IRepository<Option> optionRepository,
                             IRepository<UserAttempt> attemptRepository,
                             IMapper mapper,
                             IRepository<Test> testRepository,
                             AppDbContext context)
    {
        _answerRepository = answerRepository;
        _questionRepository = questionRepository;
        _optionRepository = optionRepository;
        _attemptRepository = attemptRepository;
        _mapper = mapper;
        _testRepository = testRepository;
        _context = context;
    }


    public async Task<IEnumerable<UserAnswerFromResultDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<UserAnswerFromResultDto> GetByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> CreateAsync(UserAttemptFromCreateDto dto,Guid AttemptId, List<Guid> correctAnswersIds)
    {
        var Attempt = _attemptRepository.GetAll().Where(x => x.Id == AttemptId);
        if ((!Attempt.Any()))
            throw new Exception("urinish mavjud emas");

        using (var transaction = await _context.Database.BeginTransactionAsync())
        {

            try
            {
                foreach (var respone in dto.Responses)
                {
                    foreach (var correctId in correctAnswersIds)
                    {
                        if (correctId == respone.OptionId)
                        {
                            await _answerRepository.CreateAsync(new()
                            {
                                QuestionId = respone.QuestionId,
                                OptionId = respone.OptionId,
                                AnsweredAt = respone.AnsweredAt,
                                UserAttemptId = AttemptId,
                            });
                        }
                    }

                }
               await transaction.CommitAsync();   
                return AttemptId;
            }
            catch (Exception)
            {
               await transaction.RollbackAsync();
                throw;
            }
        }

    }

    public async Task<UserAnswerFromResultDto> UpdateAsync(Guid Id, UserAnswerFromUpdateDto dto)
    {
        var answer = await _answerRepository.GetByIdAsync(Id);
        if (answer is null)
            throw new TestCustomException(404, "bu javob mavjud emas");

        var answerMap = _mapper.Map(dto, answer);
        var updateAnswer = await _answerRepository.UpdateAsync(answerMap);


        return _mapper.Map<UserAnswerFromResultDto>(updateAnswer);

    }

    public async Task<bool> DeleteAsync(Guid Id)
    {
        var answer = await _answerRepository.GetByIdAsync(Id);
        if (answer is null)
            throw new TestCustomException(404, "bu javob mavjud emas");

        await _answerRepository.DeleteAsync(Id);
        return true;
    }

}
