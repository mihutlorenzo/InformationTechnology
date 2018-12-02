using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EducationalPlatform.Models;

namespace EducationalPlatform.ViewModels
{
    public class NewRegisterUser
    {
        public IEnumerable<UserType> UserTypes { get; set; }
        public IEnumerable<Specialization> Specializations { get; set; }
        public IEnumerable<Group> Groups { get; set; }
        public IEnumerable<Semester> Semesters { get; set; }
        public IEnumerable<Year> Years { get; set; }
        public RegisterViewModel RegisterViewModel { get; set; }
    }
}