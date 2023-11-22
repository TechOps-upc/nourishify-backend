namespace nourishify.api.IAM.Interfaces.REST.Resources;

public record SignUpResource(string FirstName, string LastName, string Email, string Username, string Phone, string Address, string PhotoUrl, int RoleId, string Password);