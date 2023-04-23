using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MedicalResearchCenter.ViewModels.ResearchProject
{
    public class GetRPsViewModel
    {
        [Required]
        [Range(1, 50)]
        [DefaultValue(1)]
        public int PageSize { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        [DefaultValue(1)]
        public int PageNumber { get; set; }
    }
}
