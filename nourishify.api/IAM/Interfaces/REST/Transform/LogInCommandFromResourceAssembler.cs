using nourishify.api.IAM.Domain.Model.Commands;
using nourishify.api.IAM.Interfaces.REST.Resources;

namespace nourishify.api.IAM.Interfaces.REST.Transform;

public static class LogInCommandFromResourceAssembler
{
    public static LogInCommand ToCommandFromResource(LogInResource resource)
    {
        return new LogInCommand(resource.Email, resource.Password);
    }
}