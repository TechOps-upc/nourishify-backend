using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using nourishify.api.IAM.Domain.Services;
using nourishify.api.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using nourishify.api.IAM.Interfaces.REST.Resources;
using nourishify.api.IAM.Interfaces.REST.Transform;

namespace nourishify.api.IAM.Interfaces.REST;


[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class AuthenticationController : ControllerBase
{
    private readonly IUserQueryService _userQueryService;
    private readonly IUserCommandService _userCommandService;
    
    public AuthenticationController(IUserQueryService userQueryService, IUserCommandService userCommandService)
    {
        _userQueryService = userQueryService;
        _userCommandService = userCommandService;
    }
    
    /**
     * Sign up.
     * <summary>
     *     This endpoint is responsible for creating a new user.
     * </summary>
     * <param name="signUpResource">The sign up resource containing the username and password.</param>
     * <returns>A confirmation message if successful.</returns>
    */
    [HttpPost("sign-up")]
    [AllowAnonymous]
    public async Task<IActionResult> SignUp([FromBody] SignUpResource signUpResource)
    {
        var signUpCommand = SignUpCommandFromResourceAssembler.ToCommandFromResource(signUpResource);
        var createdUser = await _userCommandService.Handle(signUpCommand);
        CreatedAtAction("GetUserById", "Users", new { id = createdUser.Id }, createdUser);
        return Ok("User created successfully");
    }
    
    /**
     * Sign in.
     * <summary>
     *     This endpoint is responsible for authenticating a user.
     * </summary>
     * <param name="logInResource">The sign in resource containing the username and password.</param>
     * <returns>The authenticated user including a JWT token.</returns>
    */
    [AllowAnonymous]
    [HttpPost("log-in")]
    public async Task<IActionResult> LogIn([FromBody] LogInResource logInResource)
    {
        var signInCommand = LogInCommandFromResourceAssembler.ToCommandFromResource(logInResource);
        var authenticatedUser = await _userCommandService.Handle(signInCommand);
        var resource =
            AuthenticatedUserResourceFromEntityAssembler.ToResourceFromEntity(authenticatedUser.user,
                authenticatedUser.token);
        return Ok(resource);
    }
}