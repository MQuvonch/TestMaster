using AutoMapper;
using TestExecution.Data.IRepositories;
using TestExecution.Domain.Entities;
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
        var tests = await _testRepository.GetAllAsync();
        if (tests is null)
            throw new TestCustomException(404, "Test tuplamda teslar mavjud emas");
        return _mapper.Map<IEnumerable<TestForResultDto>>(tests);
    }

    public async Task<TestForResultDto> GetByIdAsync(Guid Id)
    {
        var test = await _testRepository.GetByIdAsync(Id);
        if (test is null)
            throw new TestCustomException(404, "test mavjud emas");

        return _mapper.Map<TestForResultDto>(test);
    }
    public async Task<TestForResultDto> UpdateAsync(Guid Id, TestForUpdateDto dto)
    {
        var test = await _testRepository.GetByIdAsync(Id);
        if (test is null)
            throw new TestCustomException(404, "test mavjud emas");

        var mapTest = _mapper.Map(dto,test);

        var updateTest = await _testRepository.UpdateAsync(mapTest);

        return _mapper.Map<TestForResultDto>(updateTest);
    }
}
