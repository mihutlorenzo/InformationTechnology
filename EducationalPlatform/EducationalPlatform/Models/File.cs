using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EducationalPlatform.Models
{
    public class File
    {
        public int FileId { get; set; }
        
        public string FileName { get; set; }

        public double Size { get; set; }

        public string FileURI { get; set; }

        public byte[] FileContent { get; set; }

        public string ContentType { get; set; }

        public DateTime UploadedDate { get; set; }

        public int? ProjectId { get; set; }

        public virtual Project Project { get; set; }

        public int? CourseId { get; set; }

        public virtual Course Course { get; set; }

        public int? StudentId { get; set; }

        public virtual Student Student { get; set; }

        public int? ProjectStatementId { get; set; }

        public virtual ProjectStatement ProjectStatement { get; set; }
    }
}