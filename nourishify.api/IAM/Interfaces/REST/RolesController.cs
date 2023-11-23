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
    [AllowAnonymous]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleResource createRoleResource)
    {
        var createRoleCommand = CreateRoleCommandFromResourceAssembler.ToCommandFromEntity(createRoleResource);
        var createdRole = await _roleCommandService.Handle(createRoleCommand);
        CreatedAtAction(nameof(GetRoleById), new { id = createdRole.RoleId }, createdRole);
        return Ok($"Role {createdRole.Name} created successfully");
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRoleById(long id)
    {
        var getRoleByIdQuery = new GetRoleByIdQuery(id);
        var role = await _roleQueryService.Handle(getRoleByIdQuery);
        var roleResource = RoleResourceFromEntityAssembler.ToResourceFromEntity(role!);
        return Ok(roleResource);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllRoles()
    {
        var getAllRolesQuery = new GetAllRolesQuery();
        var roles = await _roleQueryService.Handle(getAllRolesQuery);
        var roleResources = roles.Select(RoleResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(roleResources);
    }
}