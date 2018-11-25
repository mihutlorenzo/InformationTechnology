using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EducationalPlatform.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        [Required]
        [StringLength(100)]
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }

        [Required]
        [Display(Name = "Course Specialization")]
        public byte? SpecializationId { get; set; }
        public Specialization Specialization { get; set; }

        [Required]
        [Display(Name = "Course Semester")]
        public byte? SemesterId { get; set; }
        public Semester Semester { get; set; }

        [Required]
        [Display(Name = "Course Year")]
        public byte? YearId { get; set; }
        public Year Year { get; set; }



        public ICollection<Project> Projects { get; set; }
        public ICollection<File> Files { get; set; }

        public ICollection<Student> Students { get; set; }

        public Teacher Teacher { get; set; }

        [Required]
        [Display(Name = "Teacher of the course")]
        public Nullable<int> TeacherId { get; set; }
    }
}