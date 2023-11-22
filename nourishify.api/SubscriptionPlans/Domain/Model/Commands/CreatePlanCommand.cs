namespace nourishify.api.SubscriptionPlans.Domain.Model.Commands;

public record CreatePlanCommand(string Name, float Price,  List<string> Perks);