using AutoMapper;
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
    private readonly IMapper _mapper;

    public QuestionService(IRepository<Question> questionRepository,
                           IMapper mapper,
                           IRepository<Test> testRepository)
    {
        _questionRepository = questionRepository;
        _mapper = mapper;
        _testRepository = testRepository;
    }

    public async Task<QuestionFromResultDto> CreateAsync(QuestionFromCreateDto dto)
    {
        var tests =  _testRepository.GetAll();
        var test = tests.Where(t => t.Id == dto.TestId).FirstOrDefault();
        if (test is null)
            throw new TestCustomException(404, "test mavjud emas");

        var questionMap = _mapper.Map<Question>(dto);
        var createQuestion = await _questionRepository.CreateAsync(questionMap);

        return _mapper.Map<QuestionFromResultDto>(createQuestion);
    }

    public async Task<bool> DeleteAsync(Guid Id)
    {
        var question = await _questionRepository.GetByIdAsync(Id);
        if (question is null)
            throw new TestCustomException(404, "question mavjud emas");

        await _questionRepository.DeleteAsync(Id);
        return true;
    }

    public async Task<IEnumerable<QuestionFromResultDto>> GetAllAsync()
    {
        var questions = _questionRepository.GetAll().Select(q=>new QuestionFromResultDto
        {
            Id = q.Id,
            Text = q.Text,
            Options = q.Options.Select(o=>new OptionFromResultDto
            {
                QuestionId = o.QuestionId,
                Text = o.Text,
                IsCorrect = o.IsCorrect,    
            }).ToList()
        });
        return questions ?? throw new TestCustomException(404, "questions mavjud emas"); ;
    }

    public async Task<QuestionFromResultDto> GetByIdAsync(Guid Id)
    {
        var question = await _questionRepository.GetByIdAsync(Id);
        if (question is null)
            throw new TestCustomException(404, "question mavjud emas ");

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
}
