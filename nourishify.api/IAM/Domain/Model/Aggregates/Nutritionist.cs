namespace nourishify.api.IAM.Domain.Model.Aggregates;

public class Nutritionist : User
{
    public Nutritionist(string firstName, string lastName, string email, string username, string phone, string address, string photoUrl, long roleId, string passwordHash, int yearsOfExperience, int age, List<string> education) : base(firstName, lastName, email, username, phone, address, photoUrl, roleId, passwordHash)
    {
        YearsOfExperience = yearsOfExperience;
        Age = age;
        Education = education;
        Users = new List<User>();
    }
    
    public Nutritionist()
    {
        YearsOfExperience = 0;
        Age = 0;
        Education = new List<string>();
        Users = new List<User>();
    }
    public int YearsOfExperience { get; set; }
    public int Age { get; set; }
    public List<string> Education { get; set; }
    // Collection of users affiliated with the nutritionist
    public List<User> Users { get; set; }
}