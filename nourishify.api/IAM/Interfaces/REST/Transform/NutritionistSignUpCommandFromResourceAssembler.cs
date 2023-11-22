using nourishify.api.IAM.Domain.Model.Commands;
using nourishify.api.IAM.Interfaces.REST.Resources;

namespace nourishify.api.IAM.Interfaces.REST.Transform;

public static class NutritionistSignUpCommandFromResourceAssembler
{
    public static NutritionistSignUpCommand ToCommandFromResource(NutritionistSignUpResource resource)
    {
        return new NutritionistSignUpCommand(
            resource.FirstName,
            resource.LastName,
            resource.Email,
            resource.Username,
            resource.Phone,
            resource.Address,
            resource.PhotoUrl,
            resource.RoleId,
            resource.Password,
            resource.ExperienceYears,
            resource.Age,
            resource.Education
            );
    }
}