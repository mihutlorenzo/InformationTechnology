using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EducationalPlatform.Models;

namespace EducationalPlatform.ViewModels
{
    public class CourseFilesProjectsViewModel
    {
        public IEnumerable<File> Files { get; set; }
        public IEnumerable<ProjectStatement> ProjectsStatement { get; set; }
        public Course Course { get; set; }
        public Student Student { get; set; }
        public Teacher Teacher { get; set; }
    }
}