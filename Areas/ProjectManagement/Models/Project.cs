using System.ComponentModel.DataAnnotations;

namespace COMP2139_Lab1.Areas.ProjectManagement.Models
{
    public class Project
    {
        [Key]
        public int projectID { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "The name should consist at least 3 letters, but no more than 20")]
        public string Name { get; set; }

        public string? Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public string? Status { get; set; }

        public List<ProjectTask> Tasks { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate > StartDate)
            {
                yield return new ValidationResult("End Date must be greater than Start Date", new[] { nameof(EndDate), nameof(StartDate) });
            }
        }
    }
}
