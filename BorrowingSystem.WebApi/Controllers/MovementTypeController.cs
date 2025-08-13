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
    public class MovementTypeController(IMovementTypeService movementTypeService) : ControllerBase
    {
        private readonly IMovementTypeService _movementTypeService = movementTypeService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExistingMovementTypeDTO>>> GetAllMovementTypes()
        {
            try
            {
                var movementTypes = await _movementTypeService.GetAllMovementTypesAsync();

                return Ok(movementTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error ocurred while retrieving the movement types",
                    error = ex.Message
                });
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ExistingMovementTypeDTO>> GetMovementTypeById(Guid id)
        {
            try
            {
                var movementType = await _movementTypeService.GetMovementTypeByIdAsync(id);

                return Ok(movementType);
            }
            catch (ServiceException ex) when (ex.ErrorCode == ErrorCode.NotFound)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while retrieving the movement type",
                    error = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ExistingMovementTypeDTO>> CreateMovementType([FromBody] CreateMovementTypeDTO movementDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdMovementType = await _movementTypeService.CreateMovementTypeAsync(movementDto);

                return CreatedAtAction(nameof(GetMovementTypeById), new { id = createdMovementType.Id }, createdMovementType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while creating the movement type",
                    error = ex.Message
                });
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ExistingMovementTypeDTO>> UpdateMovementType(Guid id, [FromBody] UpdateMovementTypeDTO movementDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedMovementType = await _movementTypeService.UpdateMovementTypeAsync(id, movementDto);

                return Ok(updatedMovementType);
            }
            catch (ServiceException ex) when (ex.ErrorCode == ErrorCode.NotFound)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while updating the movement type",
                    error = ex.Message
                });
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteMovementType(Guid id)
        {
            try
            {
                await _movementTypeService.DeleteMovementTypeAsync(id);

                return NoContent();
            }
            catch (ServiceException ex) when (ex.ErrorCode == ErrorCode.NotFound)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while deleting the movement type",
                    error = ex.Message
                });
            }
        }
    }
}
