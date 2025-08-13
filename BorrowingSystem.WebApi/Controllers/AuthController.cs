using BorrowingSystem.DTOs;
using BorrowingSystem.Exceptions;
using BorrowingSystem.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BorrowingSystem.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<AuthDTO>> LoginAsync([FromBody] LoginDTO loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var loggedInUser = await _authService.LoginAsync(loginDto.Email, loginDto.PasswordHash);

                return Ok(loggedInUser);
            }
            catch (ServiceException ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while login the user",
                    error = ex.Message
                });
            }
        }
    }
}


