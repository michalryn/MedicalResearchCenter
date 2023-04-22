using System.ComponentModel.DataAnnotations;

namespace MedicalResearchCenter.ViewModels.ResearchProject
{
    public class NewRPViewModel
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        public string Description { get; set; }
        public string Manager { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
    }
}
