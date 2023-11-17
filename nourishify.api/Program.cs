using nourishify.api.IAM.Application.Internal.CommandServices;
using nourishify.api.IAM.Application.Internal.OutboundServices;
using nourishify.api.IAM.Application.Internal.QueryServices;
using nourishify.api.IAM.Domain.Repositories;
using nourishify.api.IAM.Domain.Services;
using nourishify.api.IAM.Infrastructure.Hashing.BCrypt.Services;
using nourishify.api.IAM.Infrastructure.Persitence.Repositories;
using nourishify.api.IAM.Infrastructure.Tokens.JWT.Configuration;
using nourishify.api.IAM.Infrastructure.Tokens.JWT.Services;
using nourishify.api.Shared.Domain.Repositories;
using nourishify.api.Shared.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

// TokenSettings Configuration
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();