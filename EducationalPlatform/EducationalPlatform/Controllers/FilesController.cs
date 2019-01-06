using EducationalPlatform.Models;
using EducationalPlatform.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace EducationalPlatform.Controllers
{
    public class FilesController : Controller
    {
        private ApplicationDbContext _context;

        public FilesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index(int id)
        {
            var course = _context.Courses.SingleOrDefault(c => c.CourseId == id);
            List<Models.File> files = _context.Files.Include(f => f.Course).Where(f => f.CourseId == id).ToList();

            var courseFiles = new CourseFilesViewModel
            {
                Course = course,
                Files = files
            };
            return View("FileUpload", courseFiles);
        }

        public ActionResult AddFileForProjectStatement(int id)
        {
            var projectStatement = _context.ProjectsStatement.SingleOrDefault(p => p.ProjectStatementId == id);

            return View("UploadFileForProjectStatement", projectStatement);
        }

        [HttpGet]
        public FileResult DownLoadFile(int id)
        {
            var fileFromDb = _context.Files.SingleOrDefault(file => file.FileId == id);
            byte[] bytes =(byte[])fileFromDb.FileContent;
            return File(bytes, fileFromDb.ContentType, fileFromDb.FileName);
        }

        //[HttpGet]
        //public PartialViewResult FileDetails()
        //{
        //    List<Models.File> files = _context.Files.ToList();

        //    return PartialView("FileDetails", files);


        //}

        [HttpPost]
        public ActionResult Index(int id, HttpPostedFileBase file)
        {
            int fileSize = file.ContentLength;
            string fileExt = Path.GetExtension(file.FileName).ToUpper();
            if (fileExt == ".PDF" || fileExt == ".RAR" || fileExt == ".ZIP")
            {
                //get the bytes from the uploaded file
                byte[] data = GetBytesFromFile(file);

                var fileToSave = new Models.File
                {
                    CourseId = id,
                    FileName = file.FileName,
                    FileContent = data,
                    ContentType = file.ContentType,
                    UploadedDate = DateTime.Now,
                    Size = fileSize


                };
                _context.Files.Add(fileToSave);
                _context.SaveChanges();
            }

            return RedirectToAction("CourseDetails/" + id, "Courses");
        }

        [HttpPost]
        public ActionResult UploadFileForProjectStatement(int id, HttpPostedFileBase file)
        {
            int fileSize = file.ContentLength;
            string fileExt = Path.GetExtension(file.FileName).ToUpper();
            if (fileExt == ".PDF" || fileExt == ".RAR" || fileExt == ".ZIP")
            {
                //get the bytes from the uploaded file
                byte[] data = GetBytesFromFile(file);

                var fileToSave = new Models.File
                {
                    ProjectStatementId = id,
                    FileName = file.FileName,
                    FileContent = data,
                    ContentType = file.ContentType,
                    UploadedDate = DateTime.Now,
                    Size = fileSize


                };
                _context.Files.Add(fileToSave);
                _context.SaveChanges();
            }

            return RedirectToAction("ProjectStatementDetails/" + id, "ProjectsStatement");
        }

        private byte[] GetBytesFromFile(HttpPostedFileBase file)
        {
            using (Stream inputStream = file.InputStream)
            {
                MemoryStream memoryStream = inputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }
                return memoryStream.ToArray();
            }
        }
    }
}