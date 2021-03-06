using BLL.Contracts;
using Common.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]    
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsLogic _students;

        public StudentsController(IStudentsLogic students)
        {
            _students = students;
        }

        [HttpPost("[action]")]
        [Route("Students/ImportStudents")]
        public void ImportStudents(List<StudentsDTO> model)
        {
            _students.ImportStudents(model);
        }

        [HttpPost("[action]")]
        [Route("Students/GetStudents")]
        public PagedStudentsDTO GetStudents([FromBody] StudentsDTO model, [FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            return _students.GetStudents(model, pageIndex, pageSize);
        }

        [HttpPost("[action]")]
        [Route("Students/UpdateStudent")]
        public void UpdateStudent(StudentsDTO model)
        {
            _students.UpdateStudent(model);
        }

        [HttpPost("[action]")]
        [Route("Students/DeleteStudent")]
        public void DeleteStudent(StudentsDTO model)
        {
            _students.DeleteStudent(model);
        }
    }
}
