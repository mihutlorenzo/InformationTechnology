using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EducationalPlatform.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        [Display(Name = "Name of project")]
        public string ProjectName { get; set; }
        public double? ProjectSize { get; set; }
        public string ProjectDescription { get; set; }
        public DateTime? ProjectUploadingDate { get; set; }

        [Required]
        [Display(Name = "Deadline of project")]
        public DateTime ProjectDeadline { get; set; }
        public double? Mark { get; set; }

        public Nullable<int> CourseId { get; set; }
        public Course Course { get; set; }

        //public Nullable<int> StudentId { get; set; }
        //public Student StudentThatUpload { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<File> Files { get; set; }
    }
}