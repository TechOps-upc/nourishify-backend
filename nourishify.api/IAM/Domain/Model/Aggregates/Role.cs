namespace nourishify.api.IAM.Domain.Model.Aggregates;

public partial class Role
{
    public Role(string name)
    {
        Name = name;
    }
    
    public Role()
    {
        Name = string.Empty;
    }
    
    public long RoleId { get; set; }
    public string Name { get; set; }
    
    
    //Relationship
    public IList<User> Users { get; set; } = new List<User>();
}