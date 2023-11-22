using Microsoft.AspNetCore.Mvc;
using nourishify.api.IAM.Domain.Model.Queries;
using nourishify.api.IAM.Domain.Services;
using nourishify.api.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using nourishify.api.IAM.Interfaces.REST.Resources;
using nourishify.api.IAM.Interfaces.REST.Transform;

namespace nourishify.api.IAM.Interfaces.REST;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")] 
public class RolesController : ControllerBase
{
    private readonly IRoleCommandService _roleCommandService;
    private readonly IRoleQueryService _roleQueryService;
    
    public RolesController(IRoleCommandService roleCommandService, IRoleQueryService roleQueryService)
    {
        _roleCommandService = roleCommandService;
        _roleQueryService = roleQueryService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateRole([FromBody] RoleResource roleResource)
    {
        var createRoleCommand = CreateRoleCommandFromResourceAssembler.ToCommandFromEntity(roleResource);
        var createdRole = await _roleCommandService.Handle(createRoleCommand);
        CreatedAtAction("GetRoleById", "Roles", new { id = createdRole.RoleId }, createdRole);
        return Ok("Role created successfully");
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRoleById(int id)
    {
        var getRoleByIdQuery = new GetRoleByIdQuery(id);
        var role = await _roleQueryService.Handle(getRoleByIdQuery);
        var roleResource = RoleResourceFromEntityAssembler.ToResourceFromEntity(role!);
        return Ok(roleResource);
    }
}