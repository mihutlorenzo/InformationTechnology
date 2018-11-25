using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EducationalPlatform.Models
{
    public class Specialization
    {
        public byte SpecializationId { get; set; }

        [Required]
        [StringLength(60)]
        public string SpecializationName { get; set; }
    }
}