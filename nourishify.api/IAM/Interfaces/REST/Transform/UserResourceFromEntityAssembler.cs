using nourishify.api.IAM.Domain.Model.Aggregates;
using nourishify.api.IAM.Interfaces.REST.Resources;

namespace nourishify.api.IAM.Interfaces.REST.Transform;

public static class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(User user)
    {
        return new UserResource(
            user.Id,
            user.FirstName,
            user.LastName,
            user.EmailAddress,
            user.Username);
    }
}