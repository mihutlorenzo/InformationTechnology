using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EducationalPlatform.Models
{
    public class ProjectStatement
    {
        public int ProjectStatementId { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }

        [Display(Name = "Project Description")]
        public string ProjectDescription { get; set; }

        [Display(Name = "Project Maximum Size")]
        public double? ProjectMaxSize { get; set; }

        [Required]
        [Display(Name = "Project Deadline")]
        public DateTime ProjectDeadline { get; set; }

        [Required]
        [Display(Name = "Project Creation Date")]
        public DateTime ProjectCreationDate { get; set; }

        public int? CourseId { get; set; }
        public virtual Course Course { get; set; }

        public virtual ICollection<File> Files { get; set; }
    }
}