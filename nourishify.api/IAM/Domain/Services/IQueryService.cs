using nourishify.api.IAM.Domain.Model.Aggregates;
using nourishify.api.IAM.Domain.Model.Queries;

namespace nourishify.api.IAM.Domain.Services;

public interface IQueryService
{
    Task<User?> Handle(GetUserByIdQuery query);
    Task<IEnumerable<User>> Handle(GetAllUsersQuery query);
}