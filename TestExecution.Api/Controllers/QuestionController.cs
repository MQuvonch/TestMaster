using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestExecution.Api.Models;
using TestExecution.Service.DTOs.Question;
using TestExecution.Service.Interfaces;

namespace TestExecution.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            this._questionService = questionService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Date = await _questionService.GetByIdAsync(id)
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
                Date = await _questionService.GetAllAsync()
            };
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] QuestionFromCreateDto dto)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Date = await _questionService.CreateAsync(dto),
            };
            return Ok(response);
        }
        [HttpPut("{id}")]
        
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] QuestionFromUpdateDto dto)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Date = await _questionService.UpdateAsync(id, dto),
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
                Date = await _questionService.DeleteAsync(id)
            };
            return Ok(response);
        }
    }
}
