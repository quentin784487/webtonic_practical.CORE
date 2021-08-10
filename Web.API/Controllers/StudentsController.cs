using BLL.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]    
    public class StudentsController : ControllerBase
    {
        private readonly IStudents students;

        public StudentsController(IStudents _students)
        {
            students = _students;
        }

        [HttpPost("[action]")]
        [Route("Students/ImportStudents")]
        public void ImportStudents(string base64)
        {
            students.SetStudents(base64);
        }
    }
}
