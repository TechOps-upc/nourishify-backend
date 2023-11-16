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
    public EmailAddress Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string Username { get; private set; }
    
}