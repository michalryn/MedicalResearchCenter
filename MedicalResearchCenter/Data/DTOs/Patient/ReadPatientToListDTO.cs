using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace MedicalResearchCenter.Data.DTOs.Patient
{
    public class ReadPatientToListDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Pesel { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
