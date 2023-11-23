using nourishify.api.IAM.Domain.Model.ValueObjects;

namespace nourishify.api.IAM.Domain.Model.Aggregates;

public partial class User
{
    /**
     * <summary>
     *     Updates the username of the user.
     * </summary>
     * <param name="username">The new username.</param>
     * <returns>The updated user.</returns>
     */
    public User UpdateUsername(string username)
    {
        Username = username;
        return this;
    }
    
    /**
     * <summary>
     *     Updates the password hash of the user.
     * </summary>
     * <param name="passwordHash">The new password hash.</param>
     * <returns>The updated user.</returns>
     */
    public User UpdatePasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
        return this;
    }
    
    /**
     * <summary>
     *    Updates the name of the user.
     * </summary>
     * <param name="firstName">The new first name.</param>
     * <param name="lastName">The new last name.</param>
     * <returns>The updated user.</returns>
     */
    public User UpdateName(string firstName, string lastName)
    {
        Name = new PersonName(firstName, lastName);
        return this;
    }
    
    /**
     * <summary>
     *    Updates the email of the user.
     * </summary>
     * <param name="email">The new email.</param>
     * <returns>The updated user.</returns>
     */
    public User UpdateEmail(string email)
    {
        Email = email;
        return this;
    }
}