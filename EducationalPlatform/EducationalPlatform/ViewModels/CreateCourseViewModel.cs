using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EducationalPlatform.Models;

namespace EducationalPlatform.ViewModels
{
    public class CreateCourseViewModel
    {
        public IEnumerable<Specialization> Specializations { get; set; }
        public IEnumerable<Semester> Semesters { get; set; }
        public IEnumerable<Year> Years { get; set; }
        public int TeacherId { get; set; }
        public Course Course { get; set; }
    }
}