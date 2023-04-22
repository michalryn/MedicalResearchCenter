using System.ComponentModel.DataAnnotations;

namespace MedicalResearchCenter.ViewModels.Patient
{
    public class UpdatePatientViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [MaxLength(11)]
        public string Pesel { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required(AllowEmptyStrings = true)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Region { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string City { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string PostalCode { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Street { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string UnitNumber { get; set; }
    }
}
