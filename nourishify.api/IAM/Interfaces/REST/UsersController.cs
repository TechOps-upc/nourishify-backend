using Microsoft.AspNetCore.Mvc;
using nourishify.api.IAM.Domain.Model.Commands;
using nourishify.api.IAM.Domain.Model.Queries;
using nourishify.api.IAM.Domain.Services;
using nourishify.api.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using nourishify.api.IAM.Interfaces.REST.Resources;
using nourishify.api.IAM.Interfaces.REST.Transform;
using nourishify.api.Shared.Exeptions;

namespace nourishify.api.IAM.Interfaces.REST;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]  
public class UsersController : ControllerBase
{
    private readonly IUserQueryService _userQueryService;
    private readonly IUserCommandService _userCommandService;
    
    public UsersController(IUserQueryService userQueryService, IUserCommandService userCommandService)
    {
        _userQueryService = userQueryService;
        _userCommandService = userCommandService;
    }
    
    [HttpGet("{id}", Name = "GetUserById")]
    public async Task<IActionResult> GetUserById(long id)
    {
        var getUserByIdQuery = new GetUserByIdQuery(id);
        var user = await _userQueryService.Handle(getUserByIdQuery);
        var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(user!);
        return Ok(userResource);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var getAllUsersQuery = new GetAllUsersQuery();
        var users = await _userQueryService.Handle(getAllUsersQuery);
        var userResources = users
            .Select(UserResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(userResources);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserById(long id)
    {
        try
        {
            var deleteUserCommand = new DeleteUserCommand(id);
            await _userCommandService.Handle(deleteUserCommand);
            return Ok($"User with ID {id} deleted successfully");
        }
        catch (NotFoundException)
        {
            return NotFound($"User with ID {id} not found");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting user with ID {id}\n: {ex.Message}");
        }
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUserById(long id, [FromBody] UpdateUserResource resource)
    {
        try
        {
            var updateUserCommand = UpdateUserCommandFromResourceAssembler.ToCommandFromResource(id, resource);
            await _userCommandService.Handle(updateUserCommand);
            
            var getUserByIdQuery = new GetUserByIdQuery(id);
            var user = await _userQueryService.Handle(getUserByIdQuery);

            if (user == null)
            {
                return NotFound($"User with ID {id} not found");
            }

            user.UpdatedDate = DateTimeOffset.UtcNow;
            
            var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(user);
            return Ok(userResource);
        }
        catch (NotFoundException)
        {
            return NotFound($"User with ID {id} not found");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating user with ID {id}\n: {ex.Message}");
        }
    }
}