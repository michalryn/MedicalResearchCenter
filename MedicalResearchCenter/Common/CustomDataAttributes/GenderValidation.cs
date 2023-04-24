using MedicalResearchCenter.ViewModels.Patient;
using System.ComponentModel.DataAnnotations;

namespace MedicalResearchCenter.Common.CustomDataAttributes
{
    public class GenderValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var patient = validationContext.ObjectInstance as NewPatientViewModel;
            
            if(patient?.Gender.ToLower() != "male" && patient?.Gender.ToLower() != "female")
            {
                return new ValidationResult("Invalid gender value");
            }

            return ValidationResult.Success;
        }
    }
}
