using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestExecution.Data.Contexts;
using TestExecution.Data.IRepositories;
using TestExecution.Domain.Entities;
using TestExecution.Service.DTOs.Option;
using TestExecution.Service.DTOs.Question;
using TestExecution.Service.Exceptions;
using TestExecution.Service.Interfaces;

namespace TestExecution.Service.Services;

public class QuestionService : IQuestionService
{
    private readonly IRepository<Question> _questionRepository;
    private readonly IRepository<Test> _testRepository;
    private readonly IRepository<Option> _optionRepository;
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public QuestionService(IRepository<Question> questionRepository,
                           IMapper mapper,
                           IRepository<Test> testRepository,
                           IRepository<Option> optionRepository,
                           AppDbContext context)
    {
        _questionRepository = questionRepository;
        _mapper = mapper;
        _testRepository = testRepository;
        _optionRepository = optionRepository;
        _context = context;
    }

    public async Task<Guid> CreateRangeAsync(QuestionRangeCreateDto dto)
    {
        if (!(await _context.Tests.AnyAsync(x => x.Id == dto.TestId)))
            throw new Exception("Bunday test mavjud emas");

        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                foreach (var question in dto.Questions)
                {
                    var newQuestion = await _questionRepository.CreateAsync(new()
                    {
                        TestId = dto.TestId,
                        Text = question.Text,
                    });

                    bool hasOneCorrectOption = question.Options.Count(o => o.IsCorrect) == 1;
                    if (!hasOneCorrectOption)
                        throw new Exception($"{question.Text} savolda tug'ri javob bitta emas");

                    foreach (var option in question.Options)
                    {
                        await _optionRepository.CreateAsync(new Option()
                        {
                            QuestionId = newQuestion.Id,
                            Text = option.Text,
                            IsCorrect = option.IsCorrect,
                        });
                    }
                }

                await transaction.CommitAsync();

                return dto.TestId;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        };

    }


    public async Task<bool> DeleteAsync(Guid Id)
    {
        var question = await _questionRepository.GetByIdAsync(Id);
        if (question is null)
            throw new TestCustomException(404, "question mavjud emas");

        await _questionRepository.DeleteAsync(Id);
        return true;
    }

    public async Task<IEnumerable<QuestionFromResultDto>> GetAllAsync(Guid testId)
    {
        var questions = _questionRepository.GetAll();

        var filterQuestion = questions.Where(x => x.TestId == testId)
          .Select(q => new QuestionFromResultDto
          {
              Id = q.Id,
              Text = q.Text,
              Options = q.Options.Select(o => new OptionFromResultDto
              {
                  QuestionId = o.QuestionId,
                  Text = o.Text,
              }).ToList()
          });
        return filterQuestion ?? throw new TestCustomException(404, "questions mavjud emas"); ;
    }

    public async Task<QuestionFromResultDto> GetByIdAsync(Guid Id)
    {
        var question = _questionRepository.GetAll().Include(o => o.Options).
            Where(x => x.Id == Id).FirstOrDefault();
        if (question is null)
            throw new TestCustomException(404, "question mavjud emas");

        var questionDto = new QuestionFromResultDto
        {
            Id = question.Id,
            Text = question.Text,
            Options = question.Options.Select(o => new OptionFromResultDto
            {
                QuestionId = o.QuestionId,
                Text = o.Text,
                IsCorrect = o.IsCorrect,
            }).ToList(),
        };

        return questionDto;
    }

    public async Task<QuestionFromResultDto> UpdateAsync(Guid Id, QuestionFromUpdateDto dto)
    {
        var question = await _questionRepository.GetByIdAsync(Id);
        if (question is null)
            throw new TestCustomException(404, "question mavjud emas ");

        var mapQuestion = _mapper.Map(dto, question);
        var updateQuestion = await _questionRepository.UpdateAsync(mapQuestion);

        return _mapper.Map<QuestionFromResultDto>(updateQuestion);
    }

    private bool CheckTestId(Question questions, Guid testId)
    {
        return questions.TestId == testId;
    }
}
