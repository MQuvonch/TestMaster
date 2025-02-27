using TestExecution.Service.DTOs.UserAttempt;

namespace TestExecution.Service.Interfaces;

public interface IUserAttemptService
{
    Task<IEnumerable<UserAttemptFromResultDto>> GetAllAsync();
    Task<UserAttemptFromResultDto> GetByIdAsync(Guid Id);
    Task<UserAttemptFromResultDto> CreateAsync(UserAttemptFromCreateDto dto);
    Task<UserAttemptFromResultDto> UpdateAsync(Guid Id, UserAttemptFromUpdateDto dto);
    Task<bool> DeleteAsync(Guid Id);
}
