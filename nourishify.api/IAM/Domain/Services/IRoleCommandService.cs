using nourishify.api.IAM.Domain.Model.Aggregates;
using nourishify.api.IAM.Domain.Model.Commands;

namespace nourishify.api.IAM.Domain.Services;

public interface IRoleCommandService
{
    Task<Role> Handle(CreateRoleCommand command);
}