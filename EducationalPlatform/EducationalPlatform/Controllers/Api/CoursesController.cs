using EducationalPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EducationalPlatform.Controllers.Api
{
    public class CoursesController : ApiController
    {
        private ApplicationDbContext _context;

        public CoursesController()
        {
            _context = new ApplicationDbContext();
        }

        // Get /api/courses
        public IEnumerable<Course> GetCourses()
        {

            return _context.Courses.ToList();
        }

        // Get /api/courses/1
        public IHttpActionResult GetCourse(int id)
        {
            var course = _context.Courses.SingleOrDefault(c => c.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }

        //POST /api/courses
        [HttpPost]
        public IHttpActionResult CreateCourse(Course course)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _context.Courses.Add(course);
            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + course.CourseId), course);
        }

        //PUT /api/courses/1
        [HttpPut]
        public void UpdateCourse(int id, Course course)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var courseInDb = _context.Courses.SingleOrDefault(c => c.CourseId == id);

            if (courseInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            courseInDb.CourseName = course.CourseName;
            courseInDb.CourseDescription = course.CourseDescription;
            //courseInDb.CourseSpecialization = course.CourseSpecialization;
            //courseInDb.CourseYear = course.CourseYear;
            //courseInDb.Semester = course.Semester;

            _context.SaveChanges();
        }

        //DELETE /api/student/1
        [HttpDelete]
        public void DeleteCourse(int id)
        {
            var courseInDb = _context.Courses.SingleOrDefault(c => c.CourseId == id);
            if (courseInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Courses.Remove(courseInDb);
            _context.SaveChanges();
        }
    }
}
