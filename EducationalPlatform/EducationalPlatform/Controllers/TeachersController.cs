using EducationalPlatform.Models;
using EducationalPlatform.ViewModels;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace EducationalPlatform.Controllers
{
    public class TeachersController : Controller
    {

        ApplicationDbContext _context;

        public TeachersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Teacher
        public ActionResult New()
        {
            return View();
        }


        // GET: Teacher
        public ActionResult Index(string id)
        {
            var teacher = _context.Teachers.Include(t => t.ApplicationUser).SingleOrDefault(t => t.ApplicationUserId == id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            var courses = _context.Courses.Where(c => c.TeacherId == teacher.TeacherId).Include(c => c.Specialization).Include(c => c.Semester).Include(C => C.Year).Include(x => x.Projects).ToList();

            var teacherCoursesProjects = new TeacherCoursesViewModel()
            {
                Teacher = teacher,
                Courses = courses
            };

            return View(teacherCoursesProjects);
        }

        public ActionResult EditTeacherProfile(string id)
        {
            var teacher = _context.Teachers.Include(t => t.ApplicationUser).SingleOrDefault(t => t.ApplicationUserId == id);

            return View("EditTeacherProfile", teacher);
        }

        public ActionResult TeacherProfile(string id)
        {
            var teacher = _context.Teachers.Include(s => s.ApplicationUser).SingleOrDefault(c => c.ApplicationUserId == id);

            if (teacher == null)
            {
                return HttpNotFound();
            }

            return View("TeacherProfile", teacher);
        }

        // GET: Students
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return View("EditTeacherProfile", teacher.ApplicationUserId);
            }
            //var teacher = teacherValues.Teacher;

            var teacherInDb = _context.Teachers.Include(s => s.ApplicationUser).Single(c => c.ApplicationUserId == teacher.ApplicationUserId);
            if (teacherInDb == null)
            {
                return HttpNotFound();
            }
            teacherInDb.ApplicationUser.FirstName = teacher.ApplicationUser.FirstName;
            teacherInDb.ApplicationUser.LastName = teacher.ApplicationUser.LastName;
            teacherInDb.TeacherStatus = teacher.TeacherStatus;
            teacherInDb.URIToPersonalPage = teacher.URIToPersonalPage;
            teacherInDb.InfoAboutTeacher = teacher.InfoAboutTeacher;
            _context.SaveChanges();

            return RedirectToAction("TeacherProfile/" + teacherInDb.ApplicationUserId, "Teachers");

        }
    }
}