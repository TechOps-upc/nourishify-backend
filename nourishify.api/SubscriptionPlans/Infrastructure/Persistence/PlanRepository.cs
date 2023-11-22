using nourishify.api.Shared.Infrastructure.Persistence.Configuration;
using nourishify.api.Shared.Infrastructure.Persistence.Repositories;
using nourishify.api.SubscriptionPlans.Domain.Model.Aggregates;
using nourishify.api.SubscriptionPlans.Domain.Repository;

namespace nourishify.api.SubscriptionPlans.Infrastructure.Persistence;

public class PlanRepository : BaseRepository<Plan>, IPlanRepository
{
    public PlanRepository(AppDbContext context) : base(context) { }
    
    public bool ExistsByName(string name)
    {
        return Context.Set<Plan>().Any(plan => plan.Name.Equals(name));
    }
}