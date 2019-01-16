using System;
using System.Collections.Generic;
using EducationalPlatform.Models;
using EducationalPlatform.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EducationalPlatformUnitTest
{
    [TestClass]
    public class StudentServiceTest
    {
        private const byte ExpectedYearId = 14;
        private const byte ExpectedSpecializationId = 3;
        private const byte ExpectedSemesterId = 2;

        [TestMethod]
        public void WhenCallStudentProfile_AssertReturnNotNull()
        {
            // Arrange
            var service = new StudentService();
            var student = new Student { Year = new Year { YearId = 14 } };
            var courses = new List<Course>();
            var projects = new List<Project>();

            // Act
            var students = service.StudentProfile(student, courses, projects);

            // Assert
            Assert.IsNotNull(students);
        }


        [TestMethod]
        public void WhenCallStudentProfile_AssertStudentYearIsCorrectlyProcessed()
        {
            // Arrange
            var service = new StudentService();
            var student = new Student { Year = new Year { YearId = ExpectedYearId } };
            var courses = new List<Course>();
            var projects = new List<Project>();

            // Act
            var students = service.StudentProfile(student, courses, projects);

            // Assert
            Assert.AreEqual(ExpectedYearId, students.Student.Year.YearId);
        }

        [TestMethod]
        public void WhenCallStudentProfile_AssertStudentSpecializationIsCorrectlyProcessed()
        {
            // Arrange
            var service = new StudentService();
            var student = new Student { Specialization = new Specialization { SpecializationId = ExpectedSpecializationId } };
            var courses = new List<Course>();
            var projects = new List<Project>();

            // Act
            var students = service.StudentProfile(student, courses, projects);

            // Assert
            Assert.AreEqual(ExpectedSpecializationId, students.Student.Specialization.SpecializationId);
        }

        [TestMethod]
        public void WhenCallStudentProfile_AssertStudentSemesterIsCorrectlyProcessed()
        {
            // Arrange
            var service = new StudentService();
            var student = new Student { Semester = new Semester { SemesterId = ExpectedSemesterId } };
            var courses = new List<Course>();
            var projects = new List<Project>();

            // Act
            var students = service.StudentProfile(student, courses, projects);

            // Assert
            Assert.AreEqual(ExpectedSemesterId, students.Student.Semester.SemesterId);
        }
    }
}

