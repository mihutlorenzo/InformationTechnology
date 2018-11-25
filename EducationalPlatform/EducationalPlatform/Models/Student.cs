using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationalPlatform.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        //public string Specialization { get; set; }
        //public string Group { get; set; }
        //public string Year { get; set; }
        public byte? SpecializationId { get; set; }
        public Specialization Specialization { get; set; }

        public byte? SemesterId { get; set; }
        public Semester Semester { get; set; }

        public byte? YearId { get; set; }
        public Year Year { get; set; }

        public ICollection<Course> Courses { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<File> UploadedFile { get; set; }
        // public int? CourseId { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }

}