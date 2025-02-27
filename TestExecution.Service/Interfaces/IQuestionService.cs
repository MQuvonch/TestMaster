using TestExecution.Service.DTOs.Question;
using TestExecution.Service.DTOs.Test;

namespace TestExecution.Service.Interfaces;

public interface IQuestionService
{
    Task<IEnumerable<QuestionFromResultDto>> GetAllAsync();
    Task<QuestionFromResultDto> GetByIdAsync(Guid Id);
    Task<QuestionFromResultDto> CreateAsync(QuestionFromCreateDto dto);
    Task<QuestionFromResultDto> UpdateAsync(Guid Id, QuestionFromUpdateDto dto);
    Task<bool> DeleteAsync(Guid Id);
}
