using Common.Shared.DTO;
using System.Collections.Generic;

namespace BLL.Contracts
{
    public interface ICourseTypeLogic
    {
        long SetCourseType(CourseTypeDTO model);
        CourseTypeDTO GetCourseType(CourseTypeDTO model);
        List<CourseTypeDTO> GetCourseTypes();
    }
}
