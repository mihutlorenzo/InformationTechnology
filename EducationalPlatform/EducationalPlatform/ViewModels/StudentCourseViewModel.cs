using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EducationalPlatform.Models;

namespace EducationalPlatform.ViewModels
{
    public class StudentCourseViewModel
    {
        public IEnumerable<Course> Courses { get; set; }
        public Student Student { get; set; }
    }
}