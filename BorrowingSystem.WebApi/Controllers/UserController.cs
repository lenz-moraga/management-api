using BorrowingSystem.DTOs;
using BorrowingSystem.Exceptions;
using BorrowingSystem.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BorrowingSystem.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExistingUserDTO>>> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while retrieving users",
                    error = ex.Message
                });
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ExistingUserDTO>> GetUserById(Guid id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);

                return Ok(user);
            }
            catch (ServiceException ex) when (ex.ErrorCode == ErrorCode.NotFound)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while retrieving the user",
                    error = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ExistingUserDTO>> CreateUser([FromBody] CreateUserDTO userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var newUser = await _userService.CreateUserAsync(userDto);

                return Ok(newUser);
            }
            catch (ServiceException ex) when (ex.ErrorCode == ErrorCode.BadRequest)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while creating the user",
                    error = ex.Message
                });
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ExistingUserDTO>> UpdateUser(Guid id, [FromBody] UpdateUserDTO userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedUser = await _userService.UpdateUserAsync(id, userDto);

                return Ok(updatedUser);
            }
            catch (ServiceException ex) when (ex.ErrorCode == ErrorCode.NotFound)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ServiceException ex) when (ex.ErrorCode == ErrorCode.BadRequest ||
            ex.ErrorCode == ErrorCode.Forbidden)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while updating the user",
                    error = ex.Message
                });
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);

                return NoContent();
            }
            catch (ServiceException ex) when (ex.ErrorCode == ErrorCode.NotFound)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ServiceException ex) when (ex.ErrorCode == ErrorCode.BadRequest ||
                                             ex.ErrorCode == ErrorCode.Forbidden)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while deleting the item",
                    error = ex.Message
                });
            }
        }
    }
}
