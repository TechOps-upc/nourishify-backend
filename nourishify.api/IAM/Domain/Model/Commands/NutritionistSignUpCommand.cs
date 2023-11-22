namespace nourishify.api.IAM.Domain.Model.Commands;

public record NutritionistSignUpCommand(
    string FirstName,
    string LastName,
    string Email,
    string Username,
    string Phone,
    string Address,
    string PhotoUrl,
    long RoleId,
    string Password,
    int ExperienceYears,
    int Age,
    List<string> Education
    );