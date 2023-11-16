using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using nourishify.api.IAM.Domain.Model.ValueObjects;

namespace nourishify.api.IAM.Domain.Model.Aggregates;

public class User
{
    public User(string firstName, string lastName, string email, string passwordHash, string username)
    {
        Name = new PersonName(firstName, lastName);
        Email = new EmailAddress(email);
        PasswordHash = passwordHash;
        Username = username;
    }

    public User()
    {
        Name = new PersonName();
        Email = new EmailAddress();
        PasswordHash = string.Empty;
        Username = string.Empty;
    }
    
    public long Id { get; set; }
    public PersonName Name { get; private set; }
    [EmailAddress]
    public EmailAddress Email { get; private set; }
    [JsonIgnore] public string PasswordHash { get; private set; }
    public string Username { get; private set; }
    
    // Expose properties
    public string FirstName => Name.FirstName;
    public string LastName => Name.LastName;
    public string EmailAddress => Email.Address;
    
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
        Email = new EmailAddress(email);
        return this;
    }
}