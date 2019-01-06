using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using EducationalPlatform.Models;
using EducationalPlatform.ViewModels;

namespace EducationalPlatform.Services
{
    public class StudentService
    {
        //public StudentCoursesProjectsViewModel StudentProfile(Student student, List<Course> courses, List<Project> projects)
        //{
        //    foreach (var course in courses)
        //    {
        //        var project = projects.SingleOrDefault(p => p.CourseId == course.CourseId);
        //        if (project != null)
        //        {
        //            projects.Add(project);
        //        }

        //    }

        //    var studentCoursesProjects = new StudentCoursesProjectsViewModel
        //    {
        //        Student = student,
        //        Courses = courses,
        //        Projects = projects
        //    };

        //    return studentCoursesProjects;
        //}
    }
}