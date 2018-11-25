using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EducationalPlatform.Models
{
    public class File
    {
        public int Id { get; set; }

        [Display(Name = "UploadFile")]
        public string FileName { get; set; }
        public double Size { get; set; }
        public string FileURI { get; set; }


        //public HttpPostedFileBase PDFFile { get; set; }

        public byte[] FileContent { get; set; }

        public DateTime UploadedDate { get; set; }
        public Nullable<int> ProjectId { get; set; }
        public Project Project { get; set; }
        public Nullable<int> CourseId { get; set; }
        public Course Course { get; set; }
        public Nullable<int> StudentId { get; set; }
        public Student Student { get; set; }
    }
}