using Microsoft.AspNetCore.Mvc;
using nourishify.api.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using nourishify.api.SubscriptionPlans.Domain.Model.Queries;
using nourishify.api.SubscriptionPlans.Domain.Services;
using nourishify.api.SubscriptionPlans.Interfaces.REST.Resources;
using nourishify.api.SubscriptionPlans.Interfaces.REST.Transform;

namespace nourishify.api.SubscriptionPlans.Interfaces.REST;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]  
public class PlanController : ControllerBase
{
    private readonly IPlanCommandService _planCommandService;
    private readonly IPlanQueryService _planQueryService;
    
    public PlanController(IPlanCommandService planCommandService, IPlanQueryService planQueryService)
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
}