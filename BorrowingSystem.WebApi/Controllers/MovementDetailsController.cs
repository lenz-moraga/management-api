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
    public class MovementDetailsController(IMovementDetailService movementDetailService) : ControllerBase
    {
        private readonly IMovementDetailService _movementDetailService = movementDetailService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExistingMovementDetailsDTO>>> GetAllMovementDetails()
        {
            try
            {
                var movementDetails = await _movementDetailService.GetAllMovementDetailItemsAsync();

                return Ok(movementDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new 
                { 
                    message = "An error occurred while retrieving movement details", 
                    error = ex.Message 
                });
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ExistingMovementDetailsDTO>> GetMovementDetailById(Guid id)
        {
            try
            {
                var movementDetail = await _movementDetailService.GetMovementDetailItemByIdAsync(id);

                return Ok(movementDetail);
            }
            catch (ServiceException ex) when (ex.ErrorCode == ErrorCode.NotFound)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while retrieving the movement detail",
                    error = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ExistingMovementDetailsDTO>> CreateMovementDetail([FromBody] CreateMovementDetailsDTO movementDetailDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var newMovementDetail = await _movementDetailService.CreateMovementDetailItemAsync(movementDetailDto);

                return CreatedAtAction(nameof(GetMovementDetailById), new { id = newMovementDetail.Id }, newMovementDetail);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while creating the movement detail",
                    error = ex.Message
                });
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ExistingMovementDetailsDTO>> UpdateMovementDetail(Guid id, [FromBody] UpdateMovementDetailsDTO movementDetailDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedMovementDetail = await _movementDetailService.UpdateRequestItemAsync(id, movementDetailDto);

                return Ok(updatedMovementDetail);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while updating the movement detail",
                    error = ex.Message
                });
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteMovementDetail(Guid id)
        {
            try
            {
                await _movementDetailService.DeleteRequestItemAsync(id);

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
                    message = "An error occurred while deleting the movement detail",
                    error = ex.Message
                });
            }
        }
    }
}
