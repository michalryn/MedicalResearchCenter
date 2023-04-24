using MedicalResearchCenter.Common.CustomDataAttributes;
using System.ComponentModel.DataAnnotations;

namespace MedicalResearchCenter.ViewModels.Patient
{
    public class NewPatientViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [MaxLength(11)]
        public string Pesel { get; set; }

        [Required]
        [GenderValidation]
        public string Gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string UnitNumber { get; set; }
    }
}
