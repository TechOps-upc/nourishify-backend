using nourishify.api.IAM.Domain.Model.Aggregates;
using nourishify.api.IAM.Domain.Model.Commands;
using nourishify.api.IAM.Domain.Repositories;
using nourishify.api.IAM.Domain.Services;
using nourishify.api.Shared.Domain.Repositories;

namespace nourishify.api.IAM.Application.Internal.CommandServices;

public class RoleCommandService : IRoleCommandService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRoleRepository _roleRepository;
    
    public RoleCommandService(IUnitOfWork unitOfWork, IRoleRepository roleRepository)
    {
        _unitOfWork = unitOfWork;
        _roleRepository = roleRepository;
    }
    
    public async Task Handle(CreateRoleCommand command)
    {
        var role = new Role(command.Name);
        try
        {
            await _roleRepository.AddAsync(role);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error while creating role: {ex.Message}");
        }
    }
}