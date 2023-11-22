using nourishify.api.SubscriptionPlans.Domain.Model.Aggregates;
using nourishify.api.SubscriptionPlans.Domain.Model.Queries;

namespace nourishify.api.SubscriptionPlans.Domain.Services;

public interface IPlanQueryService
{
    Task<Plan?> Handle(GetPlanByIdQuery query);
    Task<IEnumerable<Plan>> Handle(GetAllPlansQuery query);
}