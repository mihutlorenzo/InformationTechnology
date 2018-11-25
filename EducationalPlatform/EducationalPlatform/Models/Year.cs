using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EducationalPlatform.Models
{
    public class Year
    {
        public byte YearId { get; set; }

        [Range(1, 4)]
        public byte YearValue { get; set; }
    }
}