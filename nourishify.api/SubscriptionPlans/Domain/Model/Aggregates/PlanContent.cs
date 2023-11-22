namespace nourishify.api.SubscriptionPlans.Domain.Model.Aggregates;

public partial class Plan
{
    public Plan UpdateName(string name)
    {
        Name = name;
        return this;
    }
    
    public Plan UpdatePrice(float price)
    {
        Price = price;
        return this;
    }
    
    public Plan UpdatePerks(List<string> perks)
    {
        Perks = perks;
        return this;
    }
}