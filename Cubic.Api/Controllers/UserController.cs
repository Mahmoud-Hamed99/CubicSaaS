using Cubic.Application.Dtos;
using Cubic.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cubic.Api.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto dto)
        {
            var result = await _userService.CreateUser(dto);
            return Ok(result);
        }
       
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserDto dto)
        {
            var result = await _userService.UpdateUser(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
           var result= await _userService.DeleteUser(id);
            return Ok(result);
        }
    }
}
