using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EducationalPlatform.Models
{
    public class Codes
    {
        public byte Id { get; set; }

        [Display(Name = "Confirm password")]
        public string CodeValue { get; set; }
        public string TeacherLastName { get; set; }
    }
}