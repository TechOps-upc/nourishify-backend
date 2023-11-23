namespace nourishify.api.SubscriptionPlans.Interfaces.REST.Resources;

public record UpdatePlanResource(string Name, float Price, List<string> Perks);