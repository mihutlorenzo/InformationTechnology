using EducationalPlatform.Models;
using EducationalPlatform.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Data.Entity;

namespace EducationalPlatform.Controllers
{
    public class ProjectsStatementController : Controller
    {
        private ApplicationDbContext _context;

        public ProjectsStatementController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: ProjectsStatement
        [System.Web.Http.HttpGet]
        public ActionResult Index(int id,int projectId)
        {
          
            var projectStatement = _context.ProjectsStatement.Include(c=>c.Course).SingleOrDefault(c => c.ProjectStatementId == projectId);

            var student = _context.Students.SingleOrDefault(s => s.StudentId == id);

            if (projectStatement == null || student == null)
                return HttpNotFound();

            var files = _context.Files.Where(f => f.ProjectStatementId == projectId).Include(p =>p.ProjectStatement).ToList();

            var project = _context.Projects.SingleOrDefault(p => p.StudentId == id && p.ProjectStatementId == projectId);

            if(project == null)
            {
                var projectstatementFilesProjectViewModel = new ProjectStatementFilesProjectViewModel()
                {
                    ProjectStatement = projectStatement,
                    Files = files,
                    Student = student
                };
                return View("Index", projectstatementFilesProjectViewModel);
            }
            else
            {
                var projectstatementFilesProjectViewModel = new ProjectStatementFilesProjectViewModel()
                {
                    ProjectStatement = projectStatement,
                    Files = files,
                    Project = project, 
                    Student = student
                };
                return View("Index", projectstatementFilesProjectViewModel);
            }
        }

        public ActionResult CreateNewProject(int? id)
        {
            var newProject = new CreateProjectStatementViewModel
            {
                CourseId = id,
                ProjectDeadline = DateTime.Now
            };

            return View("CreateOrUpdateProject", newProject);
        }

        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save([FromUri]int id, CreateProjectStatementViewModel newProject)
        {
            var projectStatement = newProject.ProjectStatement;
            projectStatement.CourseId = id;
            if (projectStatement.ProjectStatementId == 0)
            {
                projectStatement.ProjectCreationDate = DateTime.Now;
                _context.ProjectsStatement.Add(projectStatement);
            }
            else
            {
                var projectInDb = _context.ProjectsStatement.Single(c => c.ProjectStatementId == projectStatement.ProjectStatementId);
                projectInDb.ProjectName = projectStatement.ProjectName;
                projectInDb.ProjectDescription = projectStatement.ProjectDescription;
                projectInDb.ProjectMaxSize = projectStatement.ProjectMaxSize;
                projectInDb.ProjectDeadline = projectStatement.ProjectDeadline;
            }
            _context.SaveChanges();
            return RedirectToAction("ProjectStatementDetails/" + id, "ProjectsStatement");
        }

        public ActionResult ProjectStatementDetails(int id)
        {
            var projectStatement = _context.ProjectsStatement.Include(p => p.Course).SingleOrDefault(p => p.ProjectStatementId == id);
            if (projectStatement == null)
            {
                return HttpNotFound();
            }

            var files = _context.Files.Where(f => f.ProjectStatementId == id).ToList();
            
            var projectStatementFiles = new ProjectStatementFilesViewModel
            {
                ProjectStatement = projectStatement,
                Files = files
            };
            return View("ProjectStatementDetails", projectStatementFiles);
        }

        public ActionResult EditProjectStatement(int id)
        {
            var projectStatement = _context.ProjectsStatement.SingleOrDefault(c => c.ProjectStatementId == id);
            if (projectStatement == null)
            {
                return HttpNotFound();
            }

            var updateProjectStatement = new CreateProjectStatementViewModel
            {
                CourseId = projectStatement.CourseId,
                ProjectDeadline = projectStatement.ProjectDeadline,
                ProjectStatement = projectStatement
            };

            return View("CreateOrUpdateProject", updateProjectStatement);

        }
    }
}