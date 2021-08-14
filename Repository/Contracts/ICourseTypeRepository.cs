using DAL.DataModel;
using System.Collections.Generic;

namespace Repository.Contracts
{
    public interface ICourseTypeRepository
    {
        long SetCourseType(Dictionary<string, object> model);
        PageResult GetCourseTypes();
        PageResult GetCourseTypes(Dictionary<string, object> model);
    }
}
