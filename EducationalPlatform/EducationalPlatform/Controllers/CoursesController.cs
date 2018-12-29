using EducationalPlatform.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using EducationalPlatform.ViewModels;
using System.Web.Http;

namespace EducationalPlatform.Controllers
{
    public class CoursesController : Controller
    {
        private ApplicationDbContext _context;

        public CoursesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Courses
        public ActionResult Index(int id, string studentId)
        {
            var student = _context.Students.Include(s => s.ApplicationUser).SingleOrDefault(s => s.ApplicationUserId == studentId);
            var course = _context.Courses.Include(c => c.Teacher.ApplicationUser).Include(c => c.Students).Include(c => c.Specialization).Include(c => c.Semester).Include(c => c.Year).SingleOrDefault(c => c.CourseId == id);
            var files = _context.Files.Where(f => f.CourseId == id).ToList();
            var projects = _context.Projects.Where(p => p.CourseId == id).ToList();

            if (course.Students.Contains(student))
            {
                var courseFilesProjects = new CourseFilesProjects
                {
                    Student = student,
                    Course = course,
                    Files = files,
                    Projects = projects
                };
                return View(courseFilesProjects);
            }
            else
            {
                var courseFilesProjects = new CourseFilesProjects
                {
                    Student = student,
                    Course = course,
                    Files = files
                };
                return View(courseFilesProjects);
            }

        }

        [ValidateInput(false)]
        [System.Web.Mvc.HttpPost]
        public ActionResult RegisterToCourse(string id, [FromUri] int courseId)
        {
            var course = _context.Courses.SingleOrDefault(c => c.CourseId == courseId);
            var student = _context.Students.Include(s => s.Courses).SingleOrDefault(s => s.ApplicationUserId == id);

            student.Courses.Add(course);

            _context.SaveChanges();

            var values = new RouteValueDictionary();
            values.Add("id", courseId);
            values.Add("studentId", id);

            return RedirectToAction("Index", values);

        }

        //public ActionResult Details(int id)
        //{
        //    var course = _context.Courses.SingleOrDefault(c => c.CourseId == id);

        //    if(course == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(course);
        //}
        public ActionResult Details(int id)
        {
            throw new NotImplementedException();
        }


        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(int id, CreateCourseViewModel newCourse)
        {

            var course = newCourse.Course;
            course.TeacherId = id;
            if (course.CourseId == 0)
                _context.Courses.Add(course);
            else
            {
                var courseInDb = _context.Courses.Single(c => c.CourseId == course.CourseId);
                courseInDb.CourseName = course.CourseName;
                courseInDb.CourseDescription = course.CourseDescription;
                courseInDb.SpecializationId = course.SpecializationId;
                courseInDb.SemesterId = course.SemesterId;
                courseInDb.YearId = course.YearId;
                courseInDb.TeacherId = course.TeacherId;
            }


            _context.SaveChanges();

            var teacher = _context.Teachers.SingleOrDefault(t => t.TeacherId == id);

            return RedirectToAction("Index/" + teacher.ApplicationUserId, "Teacher");
        }

        public ActionResult EditCourse(int id)
        {
            var course = _context.Courses.SingleOrDefault(c => c.CourseId == id);
            if (course == null)
            {
                return HttpNotFound();
            }


            var specialization = _context.Specializations.ToList();
            var semester = _context.Semesters.ToList();
            var year = _context.Years.ToList();
            var project = _context.Projects.Where(p => p.CourseId == id).ToList();

            if (specialization == null || semester == null || year == null)
            {
                return HttpNotFound();
            }


            var createNewCourse = new CreateCourseViewModel
            {
                TeacherId = course.TeacherId.Value,
                Specializations = specialization,
                Semesters = semester,
                Years = year,
                Course = course,
                Projects = project

            };


            return View("CreateOrUpdateCourse", createNewCourse);

        }



        public ActionResult CreateOrUpdateCourse(int? teacherId)
        {

            //var teachers = _context.Teachers.Include(c => c.ApplicationUser).ToList();
            var specialization = _context.Specializations.ToList();
            var semester = _context.Semesters.ToList();
            var year = _context.Years.ToList();

            if (specialization == null || semester == null || year == null)
            {
                return HttpNotFound();
            }

            var createNewCourse = new CreateCourseViewModel
            {
                TeacherId = teacherId.Value,
                Specializations = specialization,
                Semesters = semester,
                Years = year
            };


            return View("CreateOrUpdateCourse", createNewCourse);
        }


    }
}