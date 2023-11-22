using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using nourishify.api.IAM.Application.Internal.CommandServices;
using nourishify.api.IAM.Application.Internal.OutboundServices;
using nourishify.api.IAM.Application.Internal.QueryServices;
using nourishify.api.IAM.Domain.Repositories;
using nourishify.api.IAM.Domain.Services;
using nourishify.api.IAM.Infrastructure.Hashing.BCrypt.Services;
using nourishify.api.IAM.Infrastructure.Persitence.Repositories;
using nourishify.api.IAM.Infrastructure.Pipeline.Middleware.Extensions;
using nourishify.api.IAM.Infrastructure.Tokens.JWT.Configuration;
using nourishify.api.IAM.Infrastructure.Tokens.JWT.Services;
using nourishify.api.Shared.Domain.Repositories;
using nourishify.api.Shared.Infrastructure.Persistence.Configuration;
using nourishify.api.Shared.Infrastructure.Persistence.Repositories;
using nourishify.api.SubscriptionPlans.Application.Internal.CommandServices;
using nourishify.api.SubscriptionPlans.Application.Internal.QueryServices;
using nourishify.api.SubscriptionPlans.Domain.Repository;
using nourishify.api.SubscriptionPlans.Domain.Services;
using nourishify.api.SubscriptionPlans.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1",
            new OpenApiInfo
            {
                Title = "Nourishify.API",
                Version = "v1",
                Description = "Nourishify Platform API",
                TermsOfService = new Uri("https://nourishify.com/tos"),
                Contact = new OpenApiContact
                {
                    Name = "Nourishify",
                    Email = "contact@nourishify.com"
                },
                License = new OpenApiLicense
                {
                    Name = "Apache 2.0",
                    Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
                }
            });
        c.EnableAnnotations();

        // Add Bearer Token Authentication Security Definition
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "bearer"
        });
            
        // Add Bearer Token Authentication Requirement
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                },
                Array.Empty<string>()
            }
        });
    });

// Add Database Connection

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configure Database Context and Logging Levels

builder.Services.AddDbContext<AppDbContext>(
    options =>
    {
        if (connectionString != null)
            options.UseMySQL(connectionString)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
    });

// Configure Lowercase URLs
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Configure Dependency Injection

// Shared Bounded Context Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// IAM Bounded Context Injection Configuration
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleCommandService, RoleCommandService>();
builder.Services.AddScoped<IRoleQueryService, RoleQueryService>();

// SubscriptionPlans Bounded Context Injection Configuration
builder.Services.AddScoped<IPlanRepository, PlanRepository>();
builder.Services.AddScoped<IPlanCommandService, PlanCommandService>();
builder.Services.AddScoped<IPlanQueryService, PlanQueryService>();


// TokenSettings Configuration
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Validate Database Objects are created

using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<AppDbContext>())
{
    context?.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add CORS Middleware to ASP.NET Core Pipeline

app.UseCors("AllowAllPolicy");

// Add RequestAuthorization Middleware to ASP.NET Core Pipeline

app.UseRequestAuthorization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();