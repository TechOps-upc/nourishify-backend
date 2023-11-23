using nourishify.api.IAM.Domain.Model.Aggregates;
using nourishify.api.IAM.Domain.Model.Queries;

namespace nourishify.api.IAM.Domain.Services;

public interface IRoleQueryService
{
    Task<IEnumerable<Role>> Handle(GetAllRolesQuery query);
    Task<Role?> Handle(GetRoleByIdQuery query);
}