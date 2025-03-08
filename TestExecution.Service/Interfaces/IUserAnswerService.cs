using TestExecution.Service.DTOs.UserAnswer;
using TestExecution.Service.DTOs.UserAttempt;

namespace TestExecution.Service.Interfaces
{
    public interface IUserAnswerService
    {
        Task<IEnumerable<UserAnswerFromResultDto>> GetAllAsync();
        Task<UserAnswerFromResultDto> GetByIdAsync(Guid Id);
        Task<Guid> CreateAsync(UserAttemptFromCreateDto dto, Guid AttemptId,List<Guid> correctAnswersIds);
        Task<UserAnswerFromResultDto> UpdateAsync(Guid Id,  UserAnswerFromUpdateDto dto);
        Task<bool> DeleteAsync(Guid Id);
    } 
}
