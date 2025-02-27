using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestExecution.Api.Models;
using TestExecution.Service.DTOs.Test;
using TestExecution.Service.Interfaces;

namespace TestExecution.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Date = await _testService.GetByIdAsync(id)
            };
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetTestAll()
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Date = await _testService.GetAllAsync()
            };
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTestAsync(TestForCreateDto dto)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Date = await _testService.CreateAsync(dto)
            };
            return Ok(response);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTestAsync(Guid id, [FromBody] TestForUpdateDto dto)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Date = await _testService.UpdateAsync(id, dto)
            };
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTestAsync(Guid id)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Date = await _testService.DeleteAsync(id)
            };
            return Ok(response);    
        }

    }
}
