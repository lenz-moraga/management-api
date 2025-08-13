using BorrowingSystem.DTOs;
using BorrowingSystem.Exceptions;
using BorrowingSystem.Interfaces.Services;
using BorrowingSystem.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BorrowingSystem.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MovementController(IMovementService movementService) : ControllerBase
    {
        private readonly IMovementService _movementService = movementService;

        [HttpPost]
        public async Task<ActionResult<CreateMovementDTO>> CreateMovement([FromBody] CreateMovementDTO movementDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdMovement = await _movementService.CreateMovementAsync(movementDto);

                return CreatedAtAction(nameof(GetMovementById), new { id = createdMovement.Id }, createdMovement);
            }
            catch (ServiceException ex)
            {
                return StatusCode(ExceptionMapping.MapExceptionToControllers(ex.ErrorCode), new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while creating the movement",
                    error = ex.Message
                });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExistingMovementDTO>>> GetAllMovements()
        {
            try
            {
                var requests = await _movementService.GetAllMovementsAsync();

                return Ok(requests);
            }
            catch (ServiceException ex)
            {
                return StatusCode(ExceptionMapping.MapExceptionToControllers(ex.ErrorCode), new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while getting all the movements",
                    error = ex.Message
                });
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ExistingMovementDTO>> GetMovementById(Guid id)
        {
            try
            {
                var requests = await _movementService.GetMovementByIdAsync(id);

                return Ok(requests);
            }
            catch (ServiceException ex)
            {
                return StatusCode(ExceptionMapping.MapExceptionToControllers(ex.ErrorCode), new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while getting the request",
                    error = ex.Message
                });
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ExistingMovementDTO>> UpdateRequest(Guid id, [FromBody] UpdateMovementDTO requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var requests = await _movementService.UpdateMovementAsync(id, requestDto);

                return Ok(requests);
            }
            catch (ServiceException ex)
            {
                return StatusCode(ExceptionMapping.MapExceptionToControllers(ex.ErrorCode), new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while Updating the Movement",
                    error = ex.Message
                });
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteRequest(Guid id)
        {
            try
            {
                await _movementService.DeleteMovementAsync(id);

                return NoContent();
            }
            catch (ServiceException ex)
            {
                return StatusCode(ExceptionMapping.MapExceptionToControllers(ex.ErrorCode), new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the Movement", error = ex.Message });
            }
        }
    }
}
