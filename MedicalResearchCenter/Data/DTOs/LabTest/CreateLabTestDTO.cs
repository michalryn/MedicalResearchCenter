using System.ComponentModel.DataAnnotations;

namespace MedicalResearchCenter.Data.DTOs.LabTest
{
    public class CreateLabTestDTO
    {
        public string Name { get; set; }
        public string Unit { get; set; }
        public double NormFrom { get; set; }
        public double NormTo { get; set; }
    }
}
