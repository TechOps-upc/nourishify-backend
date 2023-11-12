using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using nourishify.api.Shared.Infrastructure.Persistence.Configuration.Extensions;

namespace nourishify.api.Shared.Infrastructure.Persistence.Configuration;

/**
 * <summary>
 *     Application Database Context
 *
 *     This class is responsible for configuring the database context for the application. 
 * </summary>
 */
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }


    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Enable Created/Updated Interceptors
        builder.AddCreatedUpdatedInterceptor();
        
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
       
        // Apply snake case naming convention
        builder.UseSnakeCaseNamingConvention();
    }
}