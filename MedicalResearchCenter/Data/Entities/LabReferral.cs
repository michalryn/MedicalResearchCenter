using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalResearchCenter.Data.Entities
{
    [Table("LabReferrals")]
    public class LabReferral
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ScheduledDate { get; set; }

        public bool Consent { get; set; }

        [Required]
        public int ResearchProjectId { get; set; }

        [ForeignKey("ResearchProjectId")]
        public virtual ResearchProject ReserachProject { get; set; }

        [Required]
        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }

        public virtual ICollection<LabReferralLabTest> LabTests { get; set; }

    }
}
