using nourishify.api.IAM.Domain.Model.Commands;
using nourishify.api.IAM.Interfaces.REST.Resources;
using Org.BouncyCastle.Crypto.Engines;

namespace nourishify.api.IAM.Interfaces.REST.Transform;

public static class UpdateUserCommandFromResourceAssembler
{
    public static UpdateUserCommand ToCommandFromResource(long id, UpdateUserResource resource)
    {
        return new UpdateUserCommand(
            id,
            resource.FirstName,
            resource.LastName,
            resource.Email,
            resource.Username,
            resource.Phone,
            resource.Address,
            resource.PhotoUrl,
            resource.RoleId
        );
    }
}