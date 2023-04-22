using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalResearchCenter.Data.Entities
{
    [Table("ResearchProjects")]
    public class ResearchProject
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Manager { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public virtual ICollection<Patient>? Patients { get; set; }

        public ResearchProject()
        {
            Patients = new List<Patient>();
        }
    }
}
