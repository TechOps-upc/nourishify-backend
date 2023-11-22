using nourishify.api.SubscriptionPlans.Domain.Model.Aggregates;
using nourishify.api.SubscriptionPlans.Interfaces.REST.Resources;

namespace nourishify.api.SubscriptionPlans.Interfaces.REST.Transform;

public static class PlanResourceFromEntityAssembler
{
    public static PlanResource ToResourceFromEntity(this Plan plan)
    {
        return new PlanResource(plan.Id, plan.Name, plan.Price, plan.Perks);
    }
}