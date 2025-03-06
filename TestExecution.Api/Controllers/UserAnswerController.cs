using Microsoft.AspNetCore.Mvc;
using TestExecution.Api.Models;
using TestExecution.Service.DTOs.UserAnswer;
using TestExecution.Service.DTOs.UserAttempt;
using TestExecution.Service.Interfaces;

namespace TestExecution.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserAnswerController : ControllerBase
    {
        private readonly IUserAnswerService _answerService;

        public UserAnswerController(IUserAnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Date = await _answerService.GetAllAsync()
            };
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Date = await _answerService.GetByIdAsync(id)
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(UserAnswerFromCreateDto dto)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Date = await _answerService.CreateAsync(dto)
            };
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, UserAnswerFromUpdateDto dto)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Date = await _answerService.UpdateAsync(id, dto)
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
                Date = await _answerService.DeleteAsync(id)
            };
            return Ok(response);
        }


    }
}
