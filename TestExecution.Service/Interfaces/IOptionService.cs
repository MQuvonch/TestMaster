using TestExecution.Service.DTOs.Option;
using TestExecution.Service.DTOs.Question;

namespace TestExecution.Service.Interfaces;

public interface IOptionService
{
    Task<IEnumerable<OptionFromResultDto>> GetAllAsync();
    Task<OptionFromResultDto> GetByIdAsync(Guid Id);
    Task<OptionFromResultDto> CreateAsync(OptionFromCreateDto dto);
    Task<OptionFromResultDto> UpdateAsync(Guid Id, OptionFromUpdateDto dto);
    Task<bool> DeleteAsync(Guid Id);
}
