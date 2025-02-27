using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestExecution.Service.DTOs.Login;

namespace TestExecution.Service.Interfaces
{
    public interface IAuthService
    {
        Task<LoginForResultDto> AuthenticateAsync(LoginDto loginDto);
        Guid TokenFromUserId();
    }
}
