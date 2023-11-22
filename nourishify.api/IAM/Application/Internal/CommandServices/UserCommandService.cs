using nourishify.api.IAM.Application.Internal.OutboundServices;
using nourishify.api.IAM.Domain.Model.Aggregates;
using nourishify.api.IAM.Domain.Model.Commands;
using nourishify.api.IAM.Domain.Repositories;
using nourishify.api.IAM.Domain.Services;
using nourishify.api.Shared.Domain.Repositories;
using nourishify.api.Shared.Exeptions;

namespace nourishify.api.IAM.Application.Internal.CommandServices;

/**
 * User Command Service.
 * <summary>
 *     <para>
 *         This class is responsible for handling the commands related to the User aggregate.
 *     </para>
 * </summary>
 */
public class UserCommandService : IUserCommandService
{
    private readonly IHashingService _hashingService;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    
    public UserCommandService(IUserRepository userRepository, ITokenService tokenService,
        IHashingService hashingService, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _hashingService = hashingService;
        _unitOfWork = unitOfWork;
    }
    
    /**
     * Handle the SignUpCommand.
     * <summary>
     *     <para>
     *         This method is responsible for handling the SignUpCommand.
     *     </para>
     * </summary>
     * <param name="command">The SignUpCommand to be handled, including username and password.</param>
     * <returns>A Task if successful, otherwise throws and exception.</returns>
     */
    public async Task<User> Handle(SignUpCommand command)
    {
        if (_userRepository.ExistsByEmail(command.Email))
            throw new Exception($"Email {command.Email} is already taken");

        var hashedPassword = _hashingService.HashPassword(command.Password);
        var user = new User(
            command.FirstName, 
            command.LastName, 
            command.Email, 
            command.Username,
            command.Phone,
            command.Address,
            command.PhotoUrl,
            command.RoleId,
            hashedPassword
                );
        try
        {
            await _userRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();
            return user;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error while creating user: {ex.Message}");
        }
    }
    
    /**
     * Handle the LogInCommand.
     * <summary>
     *     <para>
     *         This method is responsible for handling the SignInCommand.
     *     </para>
     * </summary>
     * <param name="command">The SignInCommand to be handled, including username and password.</param>
     * <returns>A tuple containing the User and the generated token if successful, otherwise throws and exception.</returns>
     */
    public async Task<(User user, string token)> Handle(LogInCommand command)
    {
        var user = await _userRepository.FindByEmailAsync(command.Email);

        if (user == null || !_hashingService.VerifyPassword(command.Password, user.PasswordHash))
            throw new Exception("Invalid email or password");

        var token = _tokenService.GenerateToken(user);

        return (user, token);
    }
    
    
    /**
     * Handle the DeleteUserCommand.
     * <summary>
     *    <para>
     *       This method is responsible for handling the DeleteUserCommand.
     *   </para>
     * </summary>
     * <param name="command">The DeleteUserCommand to be handled, including the Id of the user to be deleted.</param>
     * <returns>A Task if successful, otherwise throws and exception.</returns>
     */
    public async Task Handle(DeleteUserCommand command)
    {
        var user = await _userRepository.FindByIdAsync(command.Id);
        if (user == null)
            throw new NotFoundException();
        try
        {
            _userRepository.Remove(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    /**
     * Handle the UpdateUserCommand.
     * <summary>
     *    <para>
     *       This method is responsible for handling the UpdateUserCommand.
     *   </para>
     * </summary>
     * <param name="command">The UpdateUserCommand to be handled, including the Id of the user to be updated.</param>
     * <returns>A Task if successful, otherwise throws and exception.</returns>
     */
    public async Task Handle(UpdateUserCommand command)
    {
        var user = await _userRepository.FindByIdAsync(command.Id);
        if (user == null)
            throw new NotFoundException();
        try
        {
            user.UpdateName(command.FirstName, command.LastName);
            user.UpdateEmail(command.Email);
            user.UpdateUsername(command.Username);
            _userRepository.Update(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}