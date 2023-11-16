using nourishify.api.IAM.Domain.Model.Aggregates;
using nourishify.api.Shared.Domain.Repositories;

namespace nourishify.api.IAM.Domain.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> FindByUsernameAsync(string username);
    bool ExistsByUsername(string username);
}