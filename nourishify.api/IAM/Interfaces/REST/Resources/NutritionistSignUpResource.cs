namespace nourishify.api.IAM.Interfaces.REST.Resources;

public record NutritionistSignUpResource(string FirstName, string LastName, string Email, string Username, string Phone, string Address, string PhotoUrl, int RoleId, string Password, int ExperienceYears, int Age, List<string> Education);