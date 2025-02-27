using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestExecution.Service.DTOs.User;

namespace TestExecution.Service.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserForResultDto>> GetAllAsync();
        Task<UserForResultDto> GetByIdAsync(Guid Id);   
        Task<UserForResultDto> RegistrAsync(RegistrForCreationDto user);
        Task<UserForResultDto> UpdateAsync(Guid Id,UserForUpdateDto user);
        Task<bool> DeleteAsync(Guid Id);

    }
}
