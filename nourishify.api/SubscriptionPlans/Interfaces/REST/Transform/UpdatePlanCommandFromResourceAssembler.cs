using nourishify.api.SubscriptionPlans.Domain.Model.Commands;
using nourishify.api.SubscriptionPlans.Interfaces.REST.Resources;

namespace nourishify.api.SubscriptionPlans.Interfaces.REST.Transform;

public static class UpdatePlanCommandFromResourceAssembler
{
    public static UpdatePlanCommand ToCommandFromResource(long id, UpdatePlanResource resource)
    {
        return new UpdatePlanCommand(
            id,
            resource.Name,
            resource.Price,
            resource.Perks
        );
    }
}