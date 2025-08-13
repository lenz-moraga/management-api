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
    public class RoleController(IRoleService roleService) : ControllerBase
    {
        private readonly IRoleService _roleService = roleService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExistingRoleDTO>>> GetAllRoles()
        {
            try
            {
                var roles = await _roleService.GetAllRolesAsync();

                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while retrieving roles",
                    error = ex.Message
                });
            }
        }


        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ExistingRoleDTO>> GetRoleById(Guid id)
        {
            try
            {
                var role = await _roleService.GetRoleByIdAsync(id);

                return Ok(role);
            }
            catch (ServiceException ex) when (ex.ErrorCode == ErrorCode.NotFound)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while retrieving the role",
                    error = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ExistingRoleDTO>> CreateRole([FromBody] CreateRoleDTO roleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var newRole = await _roleService.CreateRoleAsync(roleDto);

                return CreatedAtAction(nameof(GetRoleById), new { id = newRole.Id }, newRole);
            }
            catch (ServiceException ex) when (ex.ErrorCode == ErrorCode.BadRequest)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new 
                { 
                    message = "An error occurred while creating the role", 
                    error = ex.Message 
                });
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ExistingRoleDTO>> UpdateRole(Guid id, [FromBody] UpdateRoleDTO roleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedRole = await _roleService.UpdateRoleAsync(id, roleDto);

                return Ok(updatedRole);
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
                    message = "An error occurred while updating the role", 
                    error = ex.Message 
                });
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteRole(Guid id)
        {
            try
            {
                await _roleService.DeleteRoleAsync(id);

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
                return StatusCode(500, new { message = "An error occurred while deleting the role", error = ex.Message });
            }
        }
    }
}