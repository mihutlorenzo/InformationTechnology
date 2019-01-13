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

        public ActionResult StudentsProjects(int id)
        {
            var projectStatement = _context.ProjectsStatement.SingleOrDefault(p => p.ProjectStatementId == id);
            var projects = _context.Projects.Include(p => p.Student.ApplicationUser).Where(p => p.ProjectStatementId == id).ToList();

            if (projectStatement == null)
                return HttpNotFound();

            StudentsProjectStatementProjectsViewModel viewModelToReturn = new StudentsProjectStatementProjectsViewModel
            {
                ProjectStatement = projectStatement,
                Projects = projects
            };


            return View("StudentsProjects", viewModelToReturn);
        }

        public ActionResult ProjectDetails(int id)
        {
            var project = _context.Projects.Include(p => p.Student.ApplicationUser).SingleOrDefault(p => p.ProjectId == id);

            if (project == null)
                return HttpNotFound();

            return View("ProjectDetails", project);
        }

        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(int id, Project project)
        {
            var projectInDb = _context.Projects.SingleOrDefault(c => c.ProjectId == id);
            projectInDb.ProjectMark = project.ProjectMark;
            _context.SaveChanges();

            return RedirectToAction("StudentsProjects/" + projectInDb.ProjectStatementId, "Projects");
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
        public ActionResult CreateOrUpdateProjectForStudent(int id, int projectStatementId, HttpPostedFileBase file, Project project)
        {
            
            if (project.ProjectId == 0)
            {
                project.StudentId = id;
                project.ProjectStatementId = projectStatementId;

                if (file != null)
                {
                    int fileSize = file.ContentLength;
                    string fileExt = Path.GetExtension(file.FileName).ToUpper();
                    if (fileExt == ".RAR" || fileExt == ".ZIP")
                    {
                        //get the bytes from the uploaded file
                        byte[] data = GetBytesFromFile(file);

                        project.FileName = file.FileName;
                        project.FileContent = data;
                        project.ContentType = file.ContentType;
                        project.UploadedDate = DateTime.Now;
                        project.FileSize = fileSize;
                    }
                }
                _context.Projects.Add(project);
            }
            else
            {
                var projectInDb = _context.Projects.SingleOrDefault(p => p.ProjectId == project.ProjectId);
                projectInDb.AdditionalInfo = project.AdditionalInfo;
                if(file != null)
                {
                    int fileSize = file.ContentLength;
                    string fileExt = Path.GetExtension(file.FileName).ToUpper();
                    if (fileExt == ".RAR" || fileExt == ".ZIP")
                    {
                        //get the bytes from the uploaded file
                        byte[] data = GetBytesFromFile(file);

                        projectInDb.FileName = file.FileName;
                        projectInDb.FileContent = data;
                        projectInDb.ContentType = file.ContentType;
                        projectInDb.UploadedDate = DateTime.Now;
                        projectInDb.FileSize = fileSize;
                    }
                }
            }
            _context.SaveChanges();

            var routeValues = new RouteValueDictionary
            {
                { "id", id },
                { "projectId", projectStatementId }
            };
            return RedirectToAction("Index" , "ProjectsStatement", routeValues);
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



        [System.Web.Http.HttpGet]
        public FileResult DownLoadFile(int id)
        {
            var projectFromDb = _context.Projects.SingleOrDefault(file => file.ProjectId == id);
            byte[] bytes = (byte[])projectFromDb.FileContent;
            return File(bytes, projectFromDb.ContentType, projectFromDb.FileName);
        }

    }
}