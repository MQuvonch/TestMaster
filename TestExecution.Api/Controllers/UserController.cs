using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestExecution.Api.Models;
using TestExecution.Service.DTOs.User;
using TestExecution.Service.Interfaces;

namespace TestExecution.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> GetUserAllAsync()
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Date = await _userService.GetAllAsync()
            };
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Date = await _userService.GetByIdAsync(id)
            };
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(Guid id, [FromBody] UserForUpdateDto dto)
        {
            var respose = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Date = await _userService.UpdateAsync(id, dto)
            };
            return Ok(respose);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Seccess",
                Date = await _userService.DeleteAsync(id)
            };
            return Ok(response);    
        }
    }
}
