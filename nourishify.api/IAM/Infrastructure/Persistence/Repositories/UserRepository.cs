using Microsoft.EntityFrameworkCore;
using nourishify.api.IAM.Domain.Model.Aggregates;
using nourishify.api.IAM.Domain.Repositories;
using nourishify.api.Shared.Infrastructure.Persistence.Configuration;
using nourishify.api.Shared.Infrastructure.Persistence.Repositories;

namespace nourishify.api.IAM.Infrastructure.Persitence.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context) { }

    public async Task<User?> FindByEmailAsync(string email)
    {
        return await Context.Set<User>().FirstOrDefaultAsync(user => user.Email.Equals(email));
    }

    public bool ExistsByEmail(string email)
    {
        return Context.Set<User>().Any(user => user.Email.Equals(email));
    }
}