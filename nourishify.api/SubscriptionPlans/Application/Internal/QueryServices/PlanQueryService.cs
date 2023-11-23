using nourishify.api.Shared.Exeptions;
using nourishify.api.SubscriptionPlans.Domain.Model.Aggregates;
using nourishify.api.SubscriptionPlans.Domain.Model.Queries;
using nourishify.api.SubscriptionPlans.Domain.Repository;
using nourishify.api.SubscriptionPlans.Domain.Services;

namespace nourishify.api.SubscriptionPlans.Application.Internal.QueryServices;

public class PlanQueryService : IPlanQueryService
{
    IPlanRepository _planRepository;
    
    public PlanQueryService(IPlanRepository planRepository)
    {
        _planRepository = planRepository;
    }
    
    public async Task<Plan?> Handle(GetPlanByIdQuery query)
    {
        var plan = await _planRepository.FindByIdAsync(query.Id);
        if (plan == null)
            throw new NotFoundException();
        
        return plan;
    }
    
    public async Task<IEnumerable<Plan>> Handle(GetAllPlansQuery query)
    {
        var plans = await _planRepository.ListAsync();
        return plans;
    }
}