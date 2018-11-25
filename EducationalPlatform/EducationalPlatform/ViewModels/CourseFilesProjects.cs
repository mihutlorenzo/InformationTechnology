using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EducationalPlatform.Models;

namespace EducationalPlatform.ViewModels
{
    public class CourseFilesProjects
    {
        public IEnumerable<File> Files { get; set; }
        public IEnumerable<Project> Projects { get; set; }
        public Course Course { get; set; }
        public Student Student { get; set; }
    }
}