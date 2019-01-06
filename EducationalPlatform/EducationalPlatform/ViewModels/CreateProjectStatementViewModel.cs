using EducationalPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationalPlatform.ViewModels
{
    public class CreateProjectStatementViewModel
    {
        public int? CourseId { get; set; }

        public DateTime? ProjectDeadline { get; set; }

        public ProjectStatement ProjectStatement { get; set; }

    }
}