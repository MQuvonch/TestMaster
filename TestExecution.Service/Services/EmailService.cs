using Microsoft.Extensions.Configuration;
using MimeKit;
using TestExecution.Domain.Entities;
using TestExecution.Service.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Caching.Memory;
using MailKit.Security; //
namespace TestExecution.Service.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly IMemoryCache _memoryCache;
    public EmailService(IConfiguration configuration,
                        IMemoryCache memoryCache)
    {
        _configuration = configuration.GetSection("Email");
        _memoryCache = memoryCache;
    }

    public async Task SendMessage(Message message)
    {
        var email = new MimeMessage();
        var emailAddress = _configuration["EmailAddress"];
        email.From.Add(new MailboxAddress("Siz nazoratdasiz", emailAddress));
        email.To.Add(new MailboxAddress("", message.To));
        email.Subject = message.Subject;
        email.Body = new TextPart("html") { Text = message.Body};

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_configuration["Host"], int.Parse(_configuration["Port"]), SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_configuration["EmailAddress"], _configuration["AppPassword"]);
        var response = await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }


    public async Task<bool> VerifyCodeAsync(string email, int code)
    {
        var cashedValue = _memoryCache.Get<int>(email);
        if (cashedValue == code)
        {
            return true;
        }
        return false;
    }

    public async Task<bool> VerifyEmailAsync(string email)
    {
        var randomNumber = new Random().Next(100000, 999999);

        var message = new Message()
        {
            Subject = "Do not give this code to Ohters",
            To = email,
            Body = $"{randomNumber}"
        };

        _memoryCache.Set(email, randomNumber, TimeSpan.FromMinutes(2));
        await this.SendMessage(message);

        return true;
    }
}
