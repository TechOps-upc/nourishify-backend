namespace nourishify.api.IAM.Domain.Model.Commands;

public record UpdateUserCommand(
    long Id,
    string FirstName,
    string LastName,
    string Email,
    string Username,
    string Phone,
    string Address,
    string PhotoUrl,
    int RoleId
    );