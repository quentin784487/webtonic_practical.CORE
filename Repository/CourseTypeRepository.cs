using Common.Shared.DTO;
using DAL.DataModel;
using DAL.DbContext;
using Repository.Contracts;
using System.Collections.Generic;

namespace Repository
{
    public class CourseTypeRepository : ICourseTypeRepository
    {
        private readonly IDataWrapper _dataWrapper;

        public CourseTypeRepository(IDataWrapper dataWrapper)
        {
            _dataWrapper = dataWrapper;
        }

        public long SetCourseType(Dictionary<string, object> model)
        {
            return (long)_dataWrapper.ExecuteNonQuery("sp_setCourseType", model);
        }

        public List<CourseTypeDTO> GetCourseTypes(Dictionary<string, object> model)
        {
            return _dataWrapper.ExecuteDataReader<CourseType>("sp_getCourseTypes", model, typeof(CourseType)).listItems;
        }
    }
}
