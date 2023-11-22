using nourishify.api.SubscriptionPlans.Domain.Model.Commands;
using nourishify.api.SubscriptionPlans.Interfaces.REST.Resources;

namespace nourishify.api.SubscriptionPlans.Interfaces.REST.Transform;

public static class CreatePlanCommandFromResourceAssembler
{
    public static CreatePlanCommand ToCommandFromResource(CreatePlanResource resource)
    {
        return new CreatePlanCommand(resource.Name, resource.Price, resource.Perks);
    }
}