using Microsoft.AspNetCore.Mvc;
using TestExecution.Api.Models;
using TestExecution.Service.DTOs.Option;
using TestExecution.Service.Interfaces;

namespace TestExecution.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OptionController : ControllerBase
    {
        private readonly IOptionService _optionService;

        public OptionController(IOptionService optionService )
        {
            this._optionService = optionService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var repsonse = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _optionService.GetByIdAsync(id)
            };
            return Ok(repsonse);    
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _optionService.GetAllAsync()
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] OptionFromCreateDto dto)
        {
            var repsonse = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _optionService.CreateAsync(dto)
            };
            return Ok(repsonse);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] OptionFromUpdateDto dto)
        {
            var repsonse = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _optionService.UpdateAsync(id, dto)
            };
            return Ok(repsonse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _optionService.DeleteAsync(id)
            };
            return Ok(response);
        }
    }
}
