using nourishify.api.IAM.Domain.Model.Aggregates;
using nourishify.api.IAM.Domain.Model.Commands;

namespace nourishify.api.IAM.Domain.Services;

/**
 * This interface is used to define the contract for the UserCommandService.
 * The UserCommandService is responsible for handling the commands that are
 * sent to the User aggregate.
 */
public interface IUserCommandService
{
 /**
  * This method is used to handle the SignUpCommand.
  * The SignUpCommand is used to create a new User.
  *
  * @param command The SignUpCommand that is sent to the UserCommandService.
  * @return A Task that represents the asynchronous operation.
  */
 Task<User> Handle(SignUpCommand command);
 
 /**
  * This method is used to handle the NutritionistSignUpCommand.
  * The NutritionistSignUpCommand is used to create a new Nutritionist.
  *
  * @param command The NutritionistSignUpCommand that is sent to the UserCommandService.
  * @return A Task that represents the asynchronous operation.
  */
 Task<Nutritionist> Handle(NutritionistSignUpCommand command);

 /**
  * This method is used to handle the SignInCommand.
  * The SignInCommand is used to sign in a User.
  *
  * @param command The SignInCommand that is sent to the UserCommandService.
  * @return A Task that represents the asynchronous operation.
  */
 Task<(User user, string token)> Handle(LogInCommand command);
 
 /**
  * This method is used to handle the DeleteUserCommand.
  * The DeleteUserCommand is used to delete a User.
  *
  * @param command The DeleteUserCommand that is sent to the UserCommandService.
  * @return A Task that represents the asynchronous operation.
  */
 Task Handle(DeleteUserCommand command);
 
 /**
  * This method is used to handle the UpdateUserCommand.
  * The UpdateUserCommand is used to update a User.
  *
  * @param command The UpdateUserCommand that is sent to the UserCommandService.
  * @return A Task that represents the asynchronous operation.
  */
 Task Handle(UpdateUserCommand command);
}