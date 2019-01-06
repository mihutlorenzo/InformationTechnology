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
        public ActionResult Index()
        {
            return View();
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
            return RedirectToAction("CourseDetails/" + id, "Courses");
        }

        public ActionResult ProjectStatementDetails(int projectStatementId)
        {
            var projectStatement = _context.ProjectsStatement.Include(p => p.Course).SingleOrDefault(p => p.ProjectStatementId == projectStatementId);
            if (projectStatement == null)
            {
                return HttpNotFound();
            }

            var files = _context.Files.Where(f => f.ProjectStatementId == projectStatementId).ToList();
            
            var projectStatementFiles = new ProjectStatementFilesViewModel
            {
                ProjectStatement = projectStatement,
                Files = files
            };
            return View("ProjectStatementDetails", projectStatementFiles);
        }
    }
}