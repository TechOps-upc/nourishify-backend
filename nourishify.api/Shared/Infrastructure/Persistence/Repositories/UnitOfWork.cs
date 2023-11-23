using nourishify.api.Shared.Domain.Repositories;
using nourishify.api.Shared.Infrastructure.Persistence.Configuration;

namespace nourishify.api.Shared.Infrastructure.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }
}