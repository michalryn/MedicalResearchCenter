namespace MedicalResearchCenter.Data.DTOs.LabReferral
{
    public class GetLabReferralsResponseDTO
    {
        public IEnumerable<ReadLabReferralDTO> LabReferrals { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
