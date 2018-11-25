using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EducationalPlatform.Models;

namespace EducationalPlatform.ViewModels
{
    public class CourseFilesViewModel
    {
        public IEnumerable<File> Files { get; set; }
        public Course Course { get; set; }
    }
}