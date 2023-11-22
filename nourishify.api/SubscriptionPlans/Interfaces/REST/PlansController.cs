using Microsoft.AspNetCore.Mvc;
using nourishify.api.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using nourishify.api.Shared.Exeptions;
using nourishify.api.SubscriptionPlans.Domain.Model.Commands;
using nourishify.api.SubscriptionPlans.Domain.Model.Queries;
using nourishify.api.SubscriptionPlans.Domain.Services;
using nourishify.api.SubscriptionPlans.Interfaces.REST.Resources;
using nourishify.api.SubscriptionPlans.Interfaces.REST.Transform;

namespace nourishify.api.SubscriptionPlans.Interfaces.REST;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]  
public class PlansController : ControllerBase
{
    private readonly IPlanCommandService _planCommandService;
    private readonly IPlanQueryService _planQueryService;
    
    public PlansController(IPlanCommandService planCommandService, IPlanQueryService planQueryService)
    {
        _planCommandService = planCommandService;
        _planQueryService = planQueryService;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePlan([FromBody] CreatePlanResource resource)
    {
        var createPlanCommand = CreatePlanCommandFromResourceAssembler.ToCommandFromResource(resource);
        var createdPlan = await _planCommandService.Handle(createPlanCommand);
        CreatedAtAction(nameof(GetPlanById), new { id = createdPlan.Id }, createdPlan);
        return Ok($"Plan {resource.Name} created successfully");
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPlanById(long id)
    {
        var getPlanByIdQuery = new GetPlanByIdQuery(id);
        var plan = await _planQueryService.Handle(getPlanByIdQuery);
        var planResource = PlanResourceFromEntityAssembler.ToResourceFromEntity(plan!);
        return Ok(planResource);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllPlans()
    {
        var getAllPlansQuery = new GetAllPlansQuery();
        var plans = await _planQueryService.Handle(getAllPlansQuery);
        var planResources = plans
            .Select(PlanResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(planResources);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlanById(long id)
    {
        try
        {
            var deletePlanCommand = new DeletePlanCommand(id);
            await _planCommandService.Handle(deletePlanCommand);
            return Ok($"Plan with ID {id} deleted successfully");
        }
        catch (NotFoundException)
        {
            return NotFound($"Plan with ID {id} not found");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting plan with ID {id}\n: {ex.Message}");
        }
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePlanById(long id, [FromBody] UpdatePlanResource resource)
    {
        try
        {
            var updatePlanCommand = UpdatePlanCommandFromResourceAssembler.ToCommandFromResource(id, resource);
            await _planCommandService.Handle(updatePlanCommand);
            
            var getPlanByIdQuery = new GetPlanByIdQuery(id);
            var plan = await _planQueryService.Handle(getPlanByIdQuery);
            var planResource = PlanResourceFromEntityAssembler.ToResourceFromEntity(plan!);
            return Ok(planResource);
        }
        catch (NotFoundException)
        {
            return NotFound($"Plan with ID {id} not found");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating plan with ID {id}\n: {ex.Message}");
        }
    }
}