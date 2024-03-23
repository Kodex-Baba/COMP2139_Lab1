using System.ComponentModel.DataAnnotations;

namespace COMP2139_Lab1.Areas.ProjectManagement.Models
{
    public class ProjectTask
    {
        [Key]
        public int ProjectTaskId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        // foreign key for Project
        public int projectID { get; set; }

        // Navigation property
        // This property allows for easy access to the related project entity from the Task Entity
        public Project? Project { get; set; }
    }
}
