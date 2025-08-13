using BorrowingSystem.DTOs;
using Microsoft.AspNetCore.Authorization;
using BorrowingSystem.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using BorrowingSystem.Exceptions;

namespace BorrowingSystem.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController(IItemService itemService) : ControllerBase
    {
        private readonly IItemService _itemService = itemService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExistingItemDTO>>> GetAllItems()
        {
            try
            {
                var items = await _itemService.GetAllItemsAsync();

                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while retrieving items",
                    error = ex.Message
                });
            }
        }


        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ExistingItemDTO>> GetItemById(Guid id)
        {
            try
            {
                var item = await _itemService.GetItemByIdAsync(id);

                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while retrieving the item",
                    error = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ExistingItemDTO>> CreateItem([FromBody] CreateItemDTO item)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var newItem = await _itemService.CreateItemAsync(item);

                return CreatedAtAction(nameof(GetItemById), new { id = newItem.Id }, newItem);
            }
            catch (ServiceException ex) when (ex.ErrorCode == ErrorCode.NotFound)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while creating the item",
                    error = ex.Message
                });
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ExistingItemDTO>> UpdateItem(Guid id, [FromBody] UpdateItemDTO item)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedItem = await _itemService.UpdateItemAsync(id, item);

                return Ok(updatedItem);
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
                    message = "An error occurred while updating the item",
                    error = ex.Message
                });
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteItem(Guid id)
        {
            try
            {
                await _itemService.DeleteItemAsync(id);

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
