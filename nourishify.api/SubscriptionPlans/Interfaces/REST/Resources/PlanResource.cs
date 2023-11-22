namespace nourishify.api.SubscriptionPlans.Interfaces.REST.Resources;

public record PlanResource(long Id, string Name, float Price, List<string> Perks);