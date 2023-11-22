namespace nourishify.api.IAM.Interfaces.REST.Resources;

public record UserResource(long Id, string FirstName, string LastName, string Email, string Username, string Phone, string Address, string PhotoUrl, int RoleId);