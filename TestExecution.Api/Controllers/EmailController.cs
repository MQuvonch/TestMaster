using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestExecution.Service.Interfaces;

namespace TestExecution.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet]

        public async Task<IActionResult> VerifyEmailAsync(string email)
        {
            var response = await _emailService.VerifyEmailAsync(email);
            return Ok(response);    
        }
    }
}
