using nourishify.api.SubscriptionPlans.Domain.Model.Aggregates;
using nourishify.api.SubscriptionPlans.Domain.Model.Commands;

namespace nourishify.api.SubscriptionPlans.Domain.Services;

public interface IPlanCommandService
{
    /**
     * This method is used to handle the CreatePlanCommand.
     * The CreatePlanCommand is used to create a Plan.
     *
     * @param command The CreatePlanCommand that is sent to the PlanCommandService.
     * @return A Task that represents the asynchronous operation.
     */
    Task<Plan> Handle(CreatePlanCommand command);
    
    /**
     * This method is used to handle the DeletePlanCommand.
     * The DeletePlanCommand is used to delete a Plan.
     *
     * @param command The DeletePlanCommand that is sent to the PlanCommandService.
     * @return A Task that represents the asynchronous operation.
     */
    Task Handle(DeletePlanCommand command);
 
    /**
     * This method is used to handle the UpdatePlanCommand.
     * The UpdatePlanCommand is used to update a Plan.
     *
     * @param command The UpdatePlanCommand that is sent to the PlanCommandService.
     * @return A Task that represents the asynchronous operation.
     */
    Task Handle(UpdatePlanCommand command);
}