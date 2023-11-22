using nourishify.api.IAM.Domain.Model.Aggregates;
using nourishify.api.Shared.Domain.Repositories;

namespace nourishify.api.IAM.Domain.Repositories;

public interface IRoleRepository : IBaseRepository<Role>
{
    //Task<IEnumerable<Role>> ListAsync();
    Task<Role?> FindByIdAsync(int roleId);
    //Task AddAsync(Role role);
}