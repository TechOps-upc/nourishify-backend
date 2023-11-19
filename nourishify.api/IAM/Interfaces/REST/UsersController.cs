using Microsoft.AspNetCore.Mvc;
using nourishify.api.IAM.Domain.Model.Commands;
using nourishify.api.IAM.Domain.Model.Queries;
using nourishify.api.IAM.Domain.Services;
using nourishify.api.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using nourishify.api.IAM.Interfaces.REST.Transform;

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
    
    [HttpGet("{id}")]
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
        var deleteUserCommand = new DeleteUserCommand(id);
        await _userCommandService.Handle(deleteUserCommand);
        NotFoundEx
    }
}