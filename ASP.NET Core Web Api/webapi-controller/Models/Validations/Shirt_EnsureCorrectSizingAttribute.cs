using System.ComponentModel.DataAnnotations;

namespace webapi_controller.Models.Validations
{
    public class Shirt_EnsureCorrectSizingAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (validationContext.ObjectInstance is Shirt shirt && !string.IsNullOrWhiteSpace(shirt.Gender))
            {
                if (shirt.Gender.Equals("Male", StringComparison.OrdinalIgnoreCase) && shirt.Size < 8)
                {
                    return new ValidationResult("For male's shirt, the size has to be greater than 8");
                }
                else if (shirt.Gender.Equals("Female", StringComparison.OrdinalIgnoreCase) && shirt.Size < 6)
                {
                    return new ValidationResult("For female's shirt, the size has to be greater than 6");
                }
            }
            return ValidationResult.Success;
        }
    }
}