using TestExecution.Service.DTOs.Login;

namespace TestExecution.Service.Interfaces
{
    public interface IAuthService
    {
        Task<LoginForResultDto> AuthenticateAsync(LoginDto loginDto);
        Guid TokenFromUserId();
    }
}
