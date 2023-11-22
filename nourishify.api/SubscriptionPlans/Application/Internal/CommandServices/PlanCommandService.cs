using nourishify.api.Shared.Domain.Repositories;
using nourishify.api.Shared.Exeptions;
using nourishify.api.SubscriptionPlans.Domain.Model.Aggregates;
using nourishify.api.SubscriptionPlans.Domain.Model.Commands;
using nourishify.api.SubscriptionPlans.Domain.Repository;
using nourishify.api.SubscriptionPlans.Domain.Services;

namespace nourishify.api.SubscriptionPlans.Application.Internal.CommandServices;

public class PlanCommandService : IPlanCommandService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPlanRepository _planRepository;
    
    public PlanCommandService(IUnitOfWork unitOfWork, IPlanRepository planRepository)
    {
        _unitOfWork = unitOfWork;
        _planRepository = planRepository;
    }
    
    public async Task<Plan> Handle(CreatePlanCommand command)
    {
        if (_planRepository.ExistsByName(command.Name))
            throw new Exception($"Plan with name {command.Name} already exists");
        
        var plan = new Plan(command.Name, command.Price, command.Perks);
        try
        {
            await _planRepository.AddAsync(plan);
            await _unitOfWork.CompleteAsync();
            return plan;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error while creating plan: {ex.Message}");
        }
    }
    
    public async Task Handle(DeletePlanCommand command)
    {
        var plan = await _planRepository.FindByIdAsync(command.Id);
        if (plan == null)
            throw new NotFoundException();
        
        try
        {
            _planRepository.Remove(plan);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error while deleting plan: {ex.Message}");
        }
    }
    
    public async Task Handle(UpdatePlanCommand command)
    {
        var plan = await _planRepository.FindByIdAsync(command.Id);
        if (plan == null)
            throw new NotFoundException();

        try
        {
            plan.UpdateName(command.Name);
            plan.UpdatePrice(command.Price);
            plan.UpdatePerks(command.Perks);
            _planRepository.Update(plan);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}