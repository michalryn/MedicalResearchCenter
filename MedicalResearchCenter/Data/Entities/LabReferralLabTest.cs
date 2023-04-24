using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalResearchCenter.Data.Entities
{
    [Table("LabReferralLabTests")]
    public class LabReferralLabTest
    {
        [Key]
        public int LabReferralId { get; set; }
        
        [Key]
        public int LabTestId { get; set; }

        public double Result { get; set; }

        [ForeignKey("LabReferralId")]
        public virtual LabReferral LabReferral { get; set; }

        [ForeignKey("LabTestId")]
        public virtual LabTest LabTest { get; set; }

    }
}
