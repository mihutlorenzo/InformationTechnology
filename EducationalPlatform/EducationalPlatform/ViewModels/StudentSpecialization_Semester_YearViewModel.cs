using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EducationalPlatform.Models;

namespace EducationalPlatform.ViewModels
{
    public class StudentSpecialization_Semester_YearViewModel
    {
        public IEnumerable<Specialization> Specializations { get; set; }
        public IEnumerable<Semester> Semesters { get; set; }
        public IEnumerable<Year> Years { get; set; }
        public Student Student { get; set; }
    }
}