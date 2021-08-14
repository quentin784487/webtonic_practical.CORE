using BLL.Contracts;
using Common.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseTypeLogic _courseTypeLogic;

        public CourseController(ICourseTypeLogic courseTypeLogic)
        {
            _courseTypeLogic = courseTypeLogic;
        }

        [HttpGet("[action]")]
        [Route("Course/GetCourseTypes")]
        public List<CourseTypeDTO> GetCourseTypes()
        {
            return _courseTypeLogic.GetCourseTypes();
        }
    }
}
