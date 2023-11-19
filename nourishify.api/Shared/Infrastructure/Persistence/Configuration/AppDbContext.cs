using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using nourishify.api.IAM.Domain.Model.Aggregates;
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
        
        // IAM Context
        builder.Entity<User>().ToTable("Users");
        builder.Entity<User>().HasKey(u => u.Id);
        builder.Entity<User>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().OwnsOne(u => u.Name,
        n =>
        {
            n.WithOwner().HasForeignKey("Id");
            n.Property(p => p.FirstName).HasColumnName("FirstName").HasMaxLength(50);
            n.Property(p => p.LastName).HasColumnName("LastName").HasMaxLength(50);
        });
        builder.Entity<User>().Property(u => u.Email).IsRequired().HasMaxLength(50);
        builder.Entity<User>().Property(u => u.Username).IsRequired().HasMaxLength(50);
        builder.Entity<User>().Property(u => u.PasswordHash).IsRequired().HasMaxLength(100);
        
       
        // Apply snake case naming convention
        builder.UseSnakeCaseNamingConvention();
    }
}