using EducationalPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationalPlatform.ViewModels
{
    public class StudentProjectFilesViewModel
    {
        public Student Student { get; set; }
        public Project Project { get; set; }
        public IEnumerable<File> ProjectAdditionalFiles { get; set; }
        public File StudentUploadedFile { get; set; }
    }
}