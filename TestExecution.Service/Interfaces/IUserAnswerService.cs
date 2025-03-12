using TestExecution.Service.DTOs.UserAnswer;
using TestExecution.Service.DTOs.UserAttempt;

namespace TestExecution.Service.Interfaces
{
    public interface IUserAnswerService
    {
        Task<IEnumerable<UserAnswerFromResultDto>> GetAllAsync(Guid TestId);
        Task<UserAnswerFromResultDto> GetByIdAsync(Guid Id);
        Task<Guid> CreateAsync(UserAttemptFromCreateDto dto, Guid AttemptId);
        Task<UserAnswerFromResultDto> UpdateAsync(Guid Id,  UserAnswerFromUpdateDto dto);
        Task<bool> DeleteAsync(Guid Id);
    } 
}
