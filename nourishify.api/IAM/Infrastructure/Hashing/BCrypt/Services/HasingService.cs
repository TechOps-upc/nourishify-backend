using nourishify.api.IAM.Application.Internal.OutboundServices;
using BCryptNet = BCrypt.Net.BCrypt;

namespace nourishify.api.IAM.Infrastructure.Hashing.BCrypt.Services;

public class HasingService : IHashingService
{
    /**
     * Hashes a password.
     * <param name="password">The original password</param>
     * <returns>The hashed password</returns>
     */
    public string HashPassword(string password)
    {
        return BCryptNet.HashPassword(password);
    }

    /**
     * Verifies a password.
     * <param name="password">The original password</param>
     * <param name="passwordHash">The hashed password</param>
     * <returns>True if the password is valid, false otherwise</returns>
     */
    public bool VerifyPassword(string password, string passwordHash)
    {
        return BCryptNet.Verify(password, passwordHash);
    }
}