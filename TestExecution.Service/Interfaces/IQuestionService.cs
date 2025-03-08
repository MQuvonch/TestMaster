using TestExecution.Service.DTOs.Question;
using TestExecution.Service.DTOs.Test;

namespace TestExecution.Service.Interfaces;

public interface IQuestionService
{
    Task<IEnumerable<QuestionFromResultDto>> GetAllAsync(Guid testId);
    Task<QuestionFromResultDto> GetByIdAsync(Guid Id);
    Task<Guid> CreateRangeAsync(QuestionRangeCreateDto dto);
    Task<QuestionFromResultDto> UpdateAsync(Guid Id, QuestionFromUpdateDto dto);
    Task<bool> DeleteAsync(Guid Id);
}
