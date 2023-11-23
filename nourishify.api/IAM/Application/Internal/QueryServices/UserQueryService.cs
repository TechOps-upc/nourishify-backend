using nourishify.api.IAM.Domain.Model.Aggregates;
using nourishify.api.IAM.Domain.Model.Queries;
using nourishify.api.IAM.Domain.Repositories;
using nourishify.api.IAM.Domain.Services;

namespace nourishify.api.IAM.Application.Internal.QueryServices;

public class UserQueryService : IUserQueryService
{
    private readonly IUserRepository _userRepository;

    public UserQueryService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> Handle(GetUserByIdQuery query)
    {
        return await _userRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<User>> Handle(GetAllUsersQuery query)
    {
        return await _userRepository.ListAsync();
    }
}