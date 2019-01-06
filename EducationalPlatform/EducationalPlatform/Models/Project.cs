using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EducationalPlatform.Models
{
    public class Project
    {
        public int ProjectId { get; set; }

        public int? ProjectStatementId { get; set; }

        public virtual ProjectStatement ProjectStatement { get; set; }

        public int? StudentId { get; set; }

        public virtual Student Student { get; set; }

        public double? ProjectMark { get; set; }

        public DateTime? UploadedDate { get; set; }

        public double? ProjectSize { get; set; }

        public string AdditionalInfo { get; set; }

        public string FileName { get; set; }

        public double FileSize { get; set; }

        public byte[] FileContent { get; set; }

        public string ContentType { get; set; }

    }
}