using nourishify.api.IAM.Domain.Model.Aggregates;
using nourishify.api.IAM.Domain.Repositories;
using nourishify.api.Shared.Infrastructure.Persistence.Configuration;
using nourishify.api.Shared.Infrastructure.Persistence.Repositories;

namespace nourishify.api.IAM.Infrastructure.Persitence.Repositories;

public class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    public RoleRepository(AppDbContext context) : base(context) { }
}