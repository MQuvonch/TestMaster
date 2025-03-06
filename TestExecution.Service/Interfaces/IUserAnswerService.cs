using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestExecution.Service.DTOs.UserAnswer;
using TestExecution.Service.DTOs.UserAttempt;

namespace TestExecution.Service.Interfaces
{
    public interface IUserAnswerService
    {
        Task<IEnumerable<UserAnswerFromResultDto>> GetAllAsync();
        Task<UserAnswerFromResultDto> GetByIdAsync(Guid Id);
        Task<UserAnswerFromResultDto> CreateAsync(UserAnswerFromCreateDto dto);
        Task<UserAnswerFromResultDto> UpdateAsync(Guid Id,  UserAnswerFromUpdateDto dto);
        Task<bool> DeleteAsync(Guid Id);
    } 
}
