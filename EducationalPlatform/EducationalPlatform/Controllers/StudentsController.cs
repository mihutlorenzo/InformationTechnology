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
            var students = _context.Students.Include(s => s.ApplicationUser).Include(s => s.Specialization).Include(s => s.Semester).Include(s => s.Year).ToList();
            Student theOne = new Student();
            foreach (var student in students)
            {
                if (student.ApplicationUser.Id.Equals(id))
                {
                    theOne = student;
                }
            }
            var courses = _context.Courses.Include(c => c.Teacher.ApplicationUser).Include(c => c.Specialization).Include(c => c.Semester).Include(c => c.Year).ToList();

            var studentAndCourses = new StudentCourseViewModel
            {
                Courses = courses,
                Student = theOne
            };

            return View(studentAndCourses);

        }



        public ActionResult StudentProfile(string id)
        {
            var student = _context.Students.Include(s => s.ApplicationUser).Include(s => s.Specialization).Include(s => s.Semester).Include(s => s.Year).SingleOrDefault(c => c.ApplicationUserId == id);
            if (student == null)
            {
                return HttpNotFound();
            }

            var courses = _context.Students.Where(s => s.StudentId == student.StudentId).SelectMany(c => c.Courses).Include(c => c.Teacher.ApplicationUser).Include(s => s.Specialization).Include(s => s.Semester).Include(s => s.Year).ToList();
            var projects = _context.Projects.ToList();

            var studentService = new StudentService();
            var model = studentService.StudentProfile(student, courses, projects);

            return View("StudentProfile", model);
        }
    }
}