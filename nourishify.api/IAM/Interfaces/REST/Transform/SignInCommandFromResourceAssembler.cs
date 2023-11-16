using nourishify.api.IAM.Domain.Model.Commands;
using nourishify.api.IAM.Interfaces.REST.Resources;

namespace nourishify.api.IAM.Interfaces.REST.Transform;

public static class SignInCommandFromResourceAssembler
{
    public static SignInCommand ToCommandFromResource(SignInResource resource)
    {
        return new SignInCommand(resource.Email, resource.Password);
    }
}