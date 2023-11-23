using nourishify.api.IAM.Domain.Model.Commands;
using nourishify.api.IAM.Interfaces.REST.Resources;

namespace nourishify.api.IAM.Interfaces.REST.Transform;

public static class SignUpCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
    {
        return new SignUpCommand(
            resource.FirstName,
            resource.LastName,
            resource.Email,
            resource.Username,
            resource.Phone,
            resource.Address,
            resource.PhotoUrl,
            resource.RoleId,
            resource.Password
            );
    }
}