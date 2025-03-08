using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestExecution.Data.IRepositories;
using TestExecution.Domain.Entities;
using TestExecution.Service.DTOs.Option;
using TestExecution.Service.DTOs.Question;
using TestExecution.Service.DTOs.Test;
using TestExecution.Service.Exceptions;
using TestExecution.Service.Interfaces;

namespace TestExecution.Service.Services;

public class TestService : ITestService
{
    private readonly IRepository<Test> _testRepository;
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public TestService(IRepository<Test> repository,
                    IMapper mapper,
                    IAuthService authService)
    {
        _testRepository = repository;
        _mapper = mapper;
        _authService = authService;
    }

    public async Task<TestForResultDto> CreateAsync(TestForCreateDto dto)
    {
        var newTest = _mapper.Map<Test>(dto);
        newTest.OwnerId = _authService.TokenFromUserId();

        var createTest = await _testRepository.CreateAsync(newTest);
        return _mapper.Map<TestForResultDto>(createTest);
    }

    public async Task<bool> DeleteAsync(Guid Id)
    {
        var test = await _testRepository.GetByIdAsync(Id);
        if (test is null)
            throw new TestCustomException(404, "Test mavjud emas");
        await _testRepository.DeleteAsync(Id);

        return true;
    }

    public async Task<IEnumerable<TestForResultDto>> GetAllAsync()
    {
        var customTest = await _testRepository.GetAll().Select(item => new TestForResultDto
        {
            Id = item.Id,
            Title = item.Title,
            Description = item.Description,
            Duration = item.Duration,
        }).ToListAsync();

        return customTest ?? throw new TestCustomException(404, "Test tuplamda teslar mavjud emas");
    }

    public async Task<TestForResultDto> GetByIdAsync(Guid Id)
    {
        var test =  _testRepository.GetAll().Include(q=>q.Questions).ThenInclude(o=>o.Options)
            .Where(x=>x.Id == Id).FirstOrDefault();
        if (test is null)
            throw new TestCustomException(404, "Test mavjud emas");

        var testDto = new TestForResultDto
        {
            Id = test.Id,
            Description = test.Description,
            Duration = test.Duration,
            Title = test.Title,
        };

        return testDto;
    }
    public async Task<TestForResultDto> UpdateAsync(Guid Id, TestForUpdateDto dto)
    {
        var test = await _testRepository.GetByIdAsync(Id);
        if (test is null)
            throw new TestCustomException(404, "test mavjud emas");

        var mapTest = _mapper.Map(dto, test);

        var updateTest = await _testRepository.UpdateAsync(mapTest);

        return _mapper.Map<TestForResultDto>(updateTest);
    }
}
