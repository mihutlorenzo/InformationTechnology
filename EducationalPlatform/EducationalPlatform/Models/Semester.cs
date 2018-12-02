using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EducationalPlatform.Models
{
    public class Semester
    {
        public byte SemesterId { get; set; }

        [Required]
        [StringLength(60)]
        public string SemesterName { get; set; }
    }
}