using TestExecution.Service.DTOs.Test;
using TestExecution.Service.DTOs.User;

namespace TestExecution.Service.Interfaces;

public interface ITestService
{
    Task<IEnumerable<TestForResultDto>> GetAllAsync();
    Task<TestForResultDto> GetByIdAsync(Guid Id);
    Task<TestForResultDto> CreateAsync(TestForCreateDto dto);
    Task<TestForResultDto> UpdateAsync(Guid Id, TestForUpdateDto dto);
    Task<bool> DeleteAsync(Guid Id);
}
