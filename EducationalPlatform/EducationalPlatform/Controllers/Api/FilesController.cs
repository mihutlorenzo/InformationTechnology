using EducationalPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EducationalPlatform.Controllers.Api
{
    public class FilesController : ApiController
    {
        private ApplicationDbContext _context;

        public FilesController()
        {
            _context = new ApplicationDbContext();
        }

        //DELETE /api/student/1
        [HttpDelete]
        public void DeleteFile(int id)
        {
            var fileInDb = _context.Files.SingleOrDefault(f => f.FileId == id);
            if (fileInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Files.Remove(fileInDb);
            _context.SaveChanges();
        }
    }
}
