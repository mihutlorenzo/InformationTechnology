using EducationalPlatform.Models;
using EducationalPlatform.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EducationalPlatform.Services;

namespace EducationalPlatform.Controllers
{
    public class StudentsController : Controller
    {
        ApplicationDbContext _context;

        public StudentsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }



        public ActionResult Index(string id)
        {
            var student = _context.Students.Include(s => s.ApplicationUser).FirstOrDefault(s => s.ApplicationUserId == id);
            var courses = _context.Courses.Include(c => c.Teacher.ApplicationUser).Include(c => c.Specialization).Include(c => c.Semester).Include(c => c.Year).ToList();

            var studentAndCourses = new StudentCourseViewModel
            {
                Courses = courses,
                Student = student
            };

            return View(studentAndCourses);

        }



        public ActionResult StudentProfile(string id)
        {
            var student = _context.Students.Include(s => s.ApplicationUser).Include(s => s.Specialization).Include(s => s.Group).Include(s => s.Semester).Include(s => s.Year).SingleOrDefault(c => c.ApplicationUserId == id);
            if (student == null)
            {
                return HttpNotFound();
            }

            var courses = _context.Students.Where(s => s.StudentId == student.StudentId).SelectMany(c => c.Courses).Include(c => c.Teacher.ApplicationUser).Include(s => s.Specialization).Include(s => s.Semester).Include(s => s.Year).ToList();
            //var projects = _context.Projects.ToList();

            //var studentService = new StudentService();
            //var model = studentService.StudentProfile(student, courses, projects);

            //comentat aici
            //foreach (var course in courses)
            //{
            //    var project = projects.SingleOrDefault(p => p.CourseId == course.CourseId);
            //    if (project != null)
            //    {
            //        projects.Add(project);
            //    }

            //}

            var studentCoursesProjects = new StudentCoursesProjectsViewModel
            {
                Student = student,
                Courses = courses,
               // Projects = projects
            };

            return View("StudentProfile", studentCoursesProjects);
        }



        public ActionResult EditStudentProfile(string id)
        {
            var student = _context.Students.Include(s => s.ApplicationUser).Include(s => s.Specialization).Include(s => s.Group).Include(s => s.Semester).Include(s => s.Year).SingleOrDefault(s => s.ApplicationUserId == id);
            var specializations = _context.Specializations.ToList();
            var groups = _context.Groups.ToList();
            var semesters = _context.Semesters.ToList();
            var years = _context.Years.ToList();

            if (student == null || specializations == null || groups == null || semesters == null || years == null)
            {
                return HttpNotFound();
            }
            var studentSpecializationSemesterYear = new StudentSpecializationGroupSemesterYearViewModel
            {
                Specializations = specializations,
                Groups = groups,
                Semesters = semesters,
                Years = years,
                Student = student
            };

            return View("EditStudentProfile", studentSpecializationSemesterYear);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(StudentSpecializationGroupSemesterYearViewModel studentValues)
        {
            if (!ModelState.IsValid)
            {
                //return View("EditStudentProfile", studentValues.Student.ApplicationUserId);
                return RedirectToAction("EditStudentProfile", "Students");
            }
            var student = studentValues.Student;

            var studentInDb = _context.Students.Include(s => s.ApplicationUser).Single(s => s.StudentId == student.StudentId);
            studentInDb.ApplicationUser.FirstName = student.ApplicationUser.FirstName;
            studentInDb.ApplicationUser.LastName = student.ApplicationUser.LastName;
            studentInDb.SpecializationId = student.SpecializationId;
            studentInDb.GroupId = student.GroupId;
            studentInDb.SemesterId = student.SemesterId;
            studentInDb.YearId = student.YearId;
            _context.SaveChanges();

            return RedirectToAction("StudentProfile/" + studentInDb.ApplicationUserId, "Students");
        }
    }
}