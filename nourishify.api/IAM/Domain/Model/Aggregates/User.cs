using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using nourishify.api.IAM.Domain.Model.ValueObjects;

namespace nourishify.api.IAM.Domain.Model.Aggregates;

public partial class User
{
    public User(string firstName, string lastName, string email, string username, string phone, string address, string photoUrl, long roleId, string passwordHash)
    {
        Name = new PersonName(firstName, lastName);
        Email = email;
        Username = username;
        Phone = phone;
        Address = address;
        PhotoUrl = photoUrl;
        RoleId = roleId;
        PasswordHash = passwordHash;
    }

    public User()
    {
        Name = new PersonName();
        Email = string.Empty;
        Username = string.Empty;
        Phone = string.Empty;
        Address = string.Empty;
        PhotoUrl = string.Empty;
        RoleId = 0;
        PasswordHash = string.Empty;
    }
    
    public long Id { get; set; }
    public PersonName Name { get; private set; }
    [EmailAddress]
    public string Email { get; private set; }
    [JsonIgnore] public string PasswordHash { get; private set; }
    public string Username { get; private set; }
    public string Phone { get; private set; }
    public string Address { get; private set; }
    public string PhotoUrl { get; private set; }
    public long RoleId { get; private set; }
    
    // Expose properties
    public string FirstName => Name.FirstName;
    public string LastName => Name.LastName;
    
    //Relationship
    public Role Role { get; set; }
    

    public bool IsNutritionist { get; set; }
}