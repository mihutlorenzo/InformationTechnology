using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EducationalPlatform.Models;

namespace EducationalPlatform.ViewModels
{
    public class TeacherCoursesProjectsViewModel
    {
        public IEnumerable<Course> Courses { get; set; }
        public Teacher Teacher { get; set; }
        public IEnumerable<Project> Projects { get; set; }
    }
}