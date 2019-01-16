using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EducationalPlatform.Models;
using EducationalPlatform.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EducationalPlatformUnitTest
{
    [TestClass]
    class TeacherServiceTest
    {
        private const string teacherStatus = "Acasa";
        private const string teacherLastName = "Gogu";
        private const string teacherRegisterCode = "0591";
        private const string infoAboutTeacher = "he is so boring";

        [TestMethod]
        public void WhenCallSave_AssertReturnNotNull()
        {
            // Arrange
            var service = new TeacherService();
            var teacher = new Teacher { TeacherStatus = teacherStatus, ApplicationUser = new ApplicationUser() };
            var teacherInDb = new Teacher { InfoAboutTeacher = "fastidious", ApplicationUser = new ApplicationUser() };

            // Act
            var ret = service.SaveTeacher(teacher, teacherInDb);

            // Assert
            Assert.AreEqual(teacherStatus, teacherInDb.TeacherStatus);
        }

        [TestMethod]
        public void WhenCallSave_AssertReturnNotNullRegisterCode()
        {
            // Arrange
            var service = new TeacherService();
            var teacher = new Teacher { RegisterCode = teacherRegisterCode, ApplicationUser = new ApplicationUser() };
            var teacherInDb = new Teacher { InfoAboutTeacher = infoAboutTeacher, ApplicationUser = new ApplicationUser() };

            // Act
            var ret = service.SaveTeacher(teacher, teacherInDb);

            // Assert
            Assert.AreEqual(teacherRegisterCode, teacherInDb.RegisterCode);
        }

        [TestMethod]
        public void WhenCallSave_AssertReturnNotNullStatus()
        {
            // Arrange
            var service = new TeacherService();
            var teacher = new Teacher { TeacherStatus = teacherStatus, ApplicationUser = new ApplicationUser() };
            var teacherInDb = new Teacher { InfoAboutTeacher = "he is late in class", ApplicationUser = new ApplicationUser() };

            // Act
            var ret = service.SaveTeacher(teacher, teacherInDb);

            // Assert
            Assert.AreEqual(teacherStatus, teacherInDb.TeacherStatus);
        }

        [TestMethod]
        public void WhenCallSave_AssertCorrectCopy()
        {
            // Arrange
            var service = new TeacherService();
            var teacher = new Teacher { TeacherStatus = teacherStatus, ApplicationUser = new ApplicationUser { LastName = teacherLastName } };
            var teacherInDb = new Teacher { InfoAboutTeacher = "he knows how to explain something", ApplicationUser = new ApplicationUser() };

            // Act
            var ret = service.SaveTeacher(teacher, teacherInDb);

            // Assert
            Assert.AreEqual(teacherLastName, teacherInDb.ApplicationUser.LastName);
        }

        //[ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void WhenCallSave_ExpectException()
        {
            // Arrange
            var service = new TeacherService();
            var teacher = new Teacher { TeacherStatus = teacherStatus, ApplicationUser = new ApplicationUser { LastName = teacherLastName } };

            // Act
            var ret = service.SaveTeacher(teacher, null);

            // Assert

        }
    }
}

