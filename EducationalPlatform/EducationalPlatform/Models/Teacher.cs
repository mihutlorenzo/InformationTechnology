using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationalPlatform.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string RegisterCode { get; set; }
        public string InfoAboutTeacher { get; set; }
        public string URIToPersonalPage { get; set; }
        public string TeacherStatus { get; set; }
        public ICollection<Course> Courses { get; set; }


        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}