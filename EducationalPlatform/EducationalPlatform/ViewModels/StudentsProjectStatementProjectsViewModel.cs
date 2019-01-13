using EducationalPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationalPlatform.ViewModels
{
    public class StudentsProjectStatementProjectsViewModel
    {
        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<Project> Projects { get; set; }
        public ProjectStatement ProjectStatement { get; set; }
    }
}