using EducationalPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationalPlatform.ViewModels
{
    public class ProjectStatementFilesProjectViewModel
    {
        public ProjectStatement ProjectStatement { get; set; }

        public IEnumerable<File> Files { get; set; }

        public Project Project { get; set; }

        public Student Student { get; set; }
    }
}