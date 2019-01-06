using EducationalPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationalPlatform.ViewModels
{
    public class ProjectStatementFilesViewModel
    {
        public ProjectStatement ProjectStatement { get; set; }

        public IEnumerable<File> Files { get; set; }
    }
}