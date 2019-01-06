using EducationalPlatform.Models;
using EducationalPlatform.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;

namespace EducationalPlatform.Controllers
{
    public class ProjectsController : Controller
    {
        private ApplicationDbContext _context;

        public ProjectsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        

        public ActionResult AddFiles(int id)
        {
            var project = _context.Projects.SingleOrDefault(c => c.ProjectId == id);
            if (project == null)
            {
                return HttpNotFound();
            }

            return View("CreateNewProject", project);

        }


        

        public ActionResult UploadFileForProject(int id)
        {
            var project = _context.Projects.SingleOrDefault(p => p.ProjectId == id);

            return View("UploadFilesForProject", project);
        }


        [System.Web.Mvc.HttpPost]
        public ActionResult SaveFileToDatabase(int id, HttpPostedFileBase file)
        {
            int fileSize = file.ContentLength;
            string fileExt = Path.GetExtension(file.FileName).ToUpper();
            if (fileExt == ".PDF" || fileExt == ".RAR" || fileExt == ".ZIP")
            {
                //get the bytes from the uploaded file
                byte[] data = GetBytesFromFile(file);

                var fileToSave = new Models.File
                {
                    ProjectId = id,
                    FileName = file.FileName,
                    FileContent = data,
                    ContentType = file.ContentType,
                    UploadedDate = DateTime.Now,
                    Size = fileSize
                };
                _context.Files.Add(fileToSave);
                _context.SaveChanges();
            }

            return RedirectToAction("UploadFileForProject/" + id, "Projects");
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult SaveFileToDatabaseForStudent(int id, int projectId, HttpPostedFileBase file)
        {
            int fileSize = file.ContentLength;
            String FileExt = Path.GetExtension(file.FileName).ToUpper();
            if (FileExt == ".RAR")
            {
                //get the bytes from the uploaded file
                byte[] data = GetBytesFromFile(file);


                var projectFileDB = _context.Files.FirstOrDefault(f => f.ProjectId == projectId && f.StudentId == id);
                if (projectFileDB == null)
                {
                    var fileToSave = new Models.File
                    {
                        ProjectId = projectId,
                        StudentId = id,
                        FileName = file.FileName,
                        FileContent = data,
                        UploadedDate = DateTime.Now,
                        Size = fileSize


                    };
                    _context.Files.Add(fileToSave);
                }
                else
                {
                    projectFileDB.FileName = file.FileName;
                    projectFileDB.FileContent = data;
                    projectFileDB.UploadedDate = DateTime.Now;
                    projectFileDB.Size = fileSize;
                }

                _context.SaveChanges();


            }
            var student = _context.Students.SingleOrDefault(s => s.StudentId == id);
            var values = new RouteValueDictionary();
            values.Add("projectId", projectId);
            values.Add("id", student.ApplicationUserId);
            return RedirectToAction("StudentProject", values);
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


        public ActionResult StudentProject(string id, int projectId)
        {
            var student = _context.Students.Include(s => s.Projects).SingleOrDefault(s => s.ApplicationUserId == id);
            var project = _context.Projects.SingleOrDefault(p => p.ProjectId == projectId);
            var filesForProject = _context.Files.Where(f => f.ProjectId == projectId);
            var fileUploadedByStudent = _context.Files.SingleOrDefault(f => f.StudentId == student.StudentId && f.ProjectId == projectId);
            if (!student.Projects.Contains(project))
            {
                student.Projects.Add(project);
                _context.SaveChanges();
            }

            var studentProjectFiles = new StudentProjectFilesViewModel
            {
                Student = student,
                Project = project,
                ProjectAdditionalFiles = filesForProject,
                StudentUploadedFile = fileUploadedByStudent
            };


            return View(studentProjectFiles);
        }

        //public ActionResult ViewUploadedProjects(int projectId)
        //{
        //    var project = _context.Projects.Include(p => p.Students).SingleOrDefault(p => p.Id == projectId);
        //    return View(project);
        //}



        public FileResult DownLoadFile(int id)
        {
            var fileSelected = _context.Files.SingleOrDefault(file => file.FileId == id);

            return File(fileSelected.FileContent, "App_Data/pdf", fileSelected.FileName);
        }

    }
}