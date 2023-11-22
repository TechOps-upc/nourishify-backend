namespace nourishify.api.SubscriptionPlans.Domain.Model.Aggregates;

public partial class Plan
{
    public Plan(string name, float price, List<string> perks)
    {
        Name = name;
        Price = price;
        Perks = perks;
    }

    public Plan()
    {
        Name = String.Empty;
        Price = 0;
        Perks = new List<string>();
    }
    
    public long Id { get; set; }
    public string Name { get; set; } 
    public float Price { get; set; }
    public List<string> Perks { get; set; }
}