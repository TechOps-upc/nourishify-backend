using nourishify.api.IAM.Domain.Model.Aggregates;
using nourishify.api.IAM.Domain.Model.Queries;
using nourishify.api.IAM.Domain.Repositories;
using nourishify.api.IAM.Domain.Services;

namespace nourishify.api.IAM.Application.Internal.QueryServices;

public class RoleQueryService : IRoleQueryService
{
    private readonly IRoleRepository _roleRepository;
    
    public RoleQueryService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }
    
    public async Task<Role?> Handle(GetRoleByIdQuery query)
    {
        return await _roleRepository.FindByIdAsync(query.Id);
    }
    
    public async Task<IEnumerable<Role>> Handle(GetAllRolesQuery query)
    {
        return await _roleRepository.ListAsync();
    }
}