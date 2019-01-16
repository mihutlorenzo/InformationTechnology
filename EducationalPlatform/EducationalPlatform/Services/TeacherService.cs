using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EducationalPlatform.Models;

namespace EducationalPlatform.Services
{
    public class TeacherService : Controller
    {
        // GET: TeacherService
        public Teacher SaveTeacher(Teacher teacher, Teacher teacherInDb)
        {
            if (teacherInDb == null)
            {
                throw new ArgumentNullException();
            }
            teacherInDb.ApplicationUser.FirstName = teacher.ApplicationUser.FirstName;
            teacherInDb.ApplicationUser.LastName = teacher.ApplicationUser.LastName;
            teacherInDb.TeacherStatus = teacher.TeacherStatus;
            teacherInDb.URIToPersonalPage = teacher.URIToPersonalPage;
            teacherInDb.InfoAboutTeacher = teacher.InfoAboutTeacher;
            return teacherInDb;
        }
    }
}