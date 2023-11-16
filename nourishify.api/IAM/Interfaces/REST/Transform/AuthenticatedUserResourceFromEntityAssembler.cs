using nourishify.api.IAM.Domain.Model.Aggregates;
using nourishify.api.IAM.Interfaces.REST.Resources;

namespace nourishify.api.IAM.Interfaces.REST.Transform;

public static class AuthenticatedUserResourceFromEntityAssembler
{
    public static AuthenticatedUserResource ToResourceFromEntity(User user, string token)
    {
        return new AuthenticatedUserResource(user.Id, user.EmailAddress, token);
    }
}