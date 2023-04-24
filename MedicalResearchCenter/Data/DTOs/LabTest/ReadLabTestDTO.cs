using System.ComponentModel.DataAnnotations;

namespace MedicalResearchCenter.Data.DTOs.LabTest
{
    public class ReadLabTestDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public double NormFrom { get; set; }
        public double NormTo { get; set; }
    }
}
