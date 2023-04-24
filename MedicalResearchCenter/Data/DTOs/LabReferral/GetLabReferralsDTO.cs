using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MedicalResearchCenter.Data.DTOs.LabReferral
{
    public class GetLabReferralsDTO
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int? ResearchProjectId { get; set; }
        public int? PatientId { get; set; }
    }
}
