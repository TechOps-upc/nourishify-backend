namespace nourishify.api.IAM.Domain.Model.Commands;

public record SignUpCommand(string FirstName, string LastName, string Email, string Password, string Username);