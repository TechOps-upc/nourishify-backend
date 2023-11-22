namespace nourishify.api.IAM.Domain.Model.Commands;

public record SignUpCommand(
    string FirstName,
    string LastName,
    string Email,
    string Username,
    string Phone,
    string Address,
    string PhotoUrl,
    int RoleId,
    string Password
    );