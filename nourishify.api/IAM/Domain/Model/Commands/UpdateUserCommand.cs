namespace nourishify.api.IAM.Domain.Model.Commands;

public record UpdateUserCommand(
    long Id,
    string FirstName,
    string LastName,
    string Email,
    string Username);