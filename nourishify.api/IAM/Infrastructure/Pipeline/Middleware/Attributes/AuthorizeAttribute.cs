using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using nourishify.api.IAM.Domain.Model.Aggregates;

namespace nourishify.api.IAM.Infrastructure.Pipeline.Middleware.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();

        if (allowAnonymous)
        {
            Console.WriteLine("Skipping authorization");
            return;
        }
        
        var user = (User?)context.HttpContext.Items["User"];
        
        if (user == null) context.Result = new UnauthorizedResult();
            
    }
}