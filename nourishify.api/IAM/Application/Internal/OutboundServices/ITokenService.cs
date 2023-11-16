using nourishify.api.IAM.Domain.Model.Aggregates;

namespace nourishify.api.IAM.Application.Internal.OutboundServices;

public interface ITokenService
{
    string GenerateToken(User user);
    int? ValidateToken(string token);
}