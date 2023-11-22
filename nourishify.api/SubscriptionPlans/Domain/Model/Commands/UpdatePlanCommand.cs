namespace nourishify.api.SubscriptionPlans.Domain.Model.Commands;

public record UpdatePlanCommand(long Id, string Name, float Price,  List<string> Perks);