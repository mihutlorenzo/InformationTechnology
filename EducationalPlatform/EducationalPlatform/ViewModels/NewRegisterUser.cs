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
        //public IEnumerable<string> GroupName { get; set; }
        //public IEnumerable<string> Specialization { get; set; }
        //public IEnumerable<int> Year { get; set; }
        public Codes Code { get; set; }
        public Specialization Specialization { get; set; }
        public RegisterViewModel RegisterViewModel { get; set; }
    }
}