using System.ComponentModel.DataAnnotations;

namespace GoLive.Models.ProjectDtos
{
    public class ProjectCreate
    {
        [Required]
        [StringLength(30)]
        public string ProjectName { get; set; }
        [Required]
        public string ProjectDescription { get; set; }
        [Required]
        public string ProjectExternalUrl { get; set; }
    }
}