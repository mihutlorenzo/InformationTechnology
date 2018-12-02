using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EducationalPlatform.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        [Display(Name = "Specialization")]
        public byte? SpecializationId { get; set; }
        public Specialization Specialization { get; set; }

        [Display(Name = "Group")]
        public byte? GroupId { get; set; }
        public Group Group { get; set; }

        [Display(Name = "Semester")]
        public byte? SemesterId { get; set; }
        public Semester Semester { get; set; }

        [Display(Name = "Year")]
        public byte? YearId { get; set; }
        public Year Year { get; set; }

        public ICollection<Course> Courses { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<File> UploadedFile { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }

}