using Common.Shared.DTO;
using DAL.DataModel;
using System.Collections.Generic;

namespace Repository.Contracts
{
    public interface ICourseTypeRepository
    {
        long SetCourseType(Dictionary<string, object> model);
        List<CourseTypeDTO> GetCourseTypes(Dictionary<string, object> model);
    }
}
