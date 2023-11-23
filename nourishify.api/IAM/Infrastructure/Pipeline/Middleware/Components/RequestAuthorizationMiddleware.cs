using nourishify.api.IAM.Application.Internal.OutboundServices;
using nourishify.api.IAM.Domain.Model.Queries;
using nourishify.api.IAM.Domain.Services;
using nourishify.api.IAM.Infrastructure.Pipeline.Middleware.Attributes;

namespace nourishify.api.IAM.Infrastructure.Pipeline.Middleware.Components;

public class RequestAuthorizationMiddleware
{
    private readonly RequestDelegate _next;

    public RequestAuthorizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext context,
        IUserQueryService userQueryService,
        ITokenService tokenService)
    {
        var allowAnonymous =
            context.Request.HttpContext.GetEndpoint()!.Metadata.Any(m =>
                m.GetType() == typeof(AllowAnonymousAttribute));

        if (allowAnonymous)
        {
            Console.WriteLine("Skipping authorization");
            await _next(context);
            return;
        }

        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token == null) throw new Exception("Null token");

        var userId = tokenService.ValidateToken(token);

        if (userId == null) throw new Exception("Invalid token");

        var getUserByIdQuery = new GetUserByIdQuery(userId.Value);

        var user = await userQueryService.Handle(getUserByIdQuery);
        
        context.Items["User"] = user;
        
        await _next(context);
    }
}