namespace nourishify.api.IAM.Interfaces.REST.Resources;

public record AuthenticatedUserResource(long Id, string Email, string Token);