using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestExecution.Api.Models;
using TestExecution.Service.DTOs.UserAttempt;
using TestExecution.Service.Interfaces;

namespace TestExecution.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserAttemptController : ControllerBase
    {
        private readonly IUserAttemptService _userAttemptService;

        public UserAttemptController(IUserAttemptService userAttemptService)
        {
            _userAttemptService = userAttemptService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Date = await _userAttemptService.GetByIdAsync(id)
            };
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Date = await _userAttemptService.GetAllAsync()
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] UserAttemptFromCreateDto dto)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Date = await _userAttemptService.CreateAsync(dto)
            };
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id,[FromBody] UserAttemptFromUpdateDto dto)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Date = await _userAttemptService.UpdateAsync(id, dto)
            };
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Date = await _userAttemptService.DeleteAsync(id)
            };
            return Ok(response);
        }
    }
}
