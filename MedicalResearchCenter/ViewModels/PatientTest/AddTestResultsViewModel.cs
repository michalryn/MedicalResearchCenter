using MedicalResearchCenter.Data.DTOs.PatientTest;
using System.ComponentModel.DataAnnotations;

namespace MedicalResearchCenter.ViewModels.PatientTest
{
    public class AddTestResultsViewModel
    {
        [MinLength(1)]
        public IEnumerable<AddTestResultDTO> TestResults { get; set; } 
    }
}
