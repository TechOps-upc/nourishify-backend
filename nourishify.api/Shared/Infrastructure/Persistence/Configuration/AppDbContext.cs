using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using nourishify.api.IAM.Domain.Model.Aggregates;
using nourishify.api.Shared.Converters;
using nourishify.api.Shared.Infrastructure.Persistence.Configuration.Extensions;
using nourishify.api.SubscriptionPlans.Domain.Model.Aggregates;

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
        
        // Subscription Plans Context
        // Plans
        builder.Entity<Plan>().ToTable("Plans");
        builder.Entity<Plan>().HasKey(p => p.Id);
        builder.Entity<Plan>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Plan>().Property(p => p.Name).IsRequired().HasMaxLength(50);
        builder.Entity<Plan>().Property(p => p.Price).IsRequired();
        builder.Entity<Plan>()
            .Property(p => p.Perks)
            .HasConversion(new StringListConverter());
        
        // IAM Context
        // Users
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
        builder.Entity<User>().Property(u => u.Phone).IsRequired().HasMaxLength(20);
        builder.Entity<User>().Property(u => u.Address).IsRequired().HasMaxLength(100);
        builder.Entity<User>().Property(u => u.PhotoUrl).HasMaxLength(100);
        builder.Entity<User>().Property(u => u.RoleId).IsRequired();
        builder.Entity<User>().Property(u => u.PasswordHash).IsRequired().HasMaxLength(100);
        // Nutritionists
        builder.Entity<Nutritionist>().ToTable("Nutritionists");
        builder.Entity<Nutritionist>().HasBaseType<User>(); // Ensure base User properties are shared
        builder.Entity<Nutritionist>().Property(n => n.YearsOfExperience);
        builder.Entity<Nutritionist>().Property(n => n.Age);
        // Apply the value converter to the Education property
        builder.Entity<Nutritionist>()
            .Property(n => n.Education)
            .HasConversion(new StringListConverter());
        //Roles
        builder.Entity<Role>().ToTable("Roles");
        builder.Entity<Role>().HasKey(r => r.RoleId);
        builder.Entity<Role>().Property(r => r.RoleId).IsRequired().ValueGeneratedOnAdd();
        // Define the relationship between User and Role
        builder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId);
        // Navigation property in the Role entity to access the list of users with this role
        builder.Entity<Role>()
            .HasMany(r => r.Users)
            .WithOne(u => u.Role)
            .HasForeignKey(u => u.RoleId);
        
        
        // Apply snake case naming convention
        builder.UseSnakeCaseNamingConvention();
    }
}