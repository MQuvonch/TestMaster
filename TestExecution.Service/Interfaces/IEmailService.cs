using TestExecution.Domain.Entities;

namespace TestExecution.Service.Interfaces;

public interface IEmailService
{
    public Task SendMessage(Message message);

    public Task<bool> VerifyEmailAsync(string email);
    public Task<bool> VerifyCodeAsync(string email, int code);

}
