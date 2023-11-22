namespace nourishify.api.SubscriptionPlans.Interfaces.REST.Resources;

public record CreatePlanResource(string Name, float Price, List<string> Perks);