using nourishify.api.IAM.Domain.Model.Commands;
using nourishify.api.IAM.Interfaces.REST.Resources;

namespace nourishify.api.IAM.Interfaces.REST.Transform;

public static class CreateRoleCommandFromResourceAssembler
{
    public static CreateRoleCommand ToCommandFromEntity(CreateRoleResource role)
    {
        return new CreateRoleCommand(role.Name);
    }
}