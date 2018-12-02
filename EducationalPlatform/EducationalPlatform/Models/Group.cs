using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EducationalPlatform.Models
{
    public class Group
    {
        public byte GroupId { get; set; }

        [Required]
        [StringLength(60)]
        public string GroupName { get; set; }
    }
}