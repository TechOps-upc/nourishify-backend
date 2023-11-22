using nourishify.api.Shared.Domain.Repositories;
using nourishify.api.SubscriptionPlans.Domain.Model.Aggregates;

namespace nourishify.api.SubscriptionPlans.Domain.Repository;

public interface IPlanRepository : IBaseRepository<Plan>
{
    bool ExistsByName(string name);
}