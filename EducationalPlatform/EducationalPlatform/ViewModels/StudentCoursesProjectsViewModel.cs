using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EducationalPlatform.Models;

namespace EducationalPlatform.ViewModels
{
    public class StudentCoursesProjectsViewModel
    {
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Project> Projects { get; set; }
        public Student Student { get; set; }
    }
}