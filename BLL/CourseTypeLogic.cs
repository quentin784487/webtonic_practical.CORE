using BLL.Contracts;
using Common.Shared.DTO;
using Repository.Contracts;
using System.Collections.Generic;

namespace BLL
{
    public class CourseTypeLogic : ICourseTypeLogic
    {
        private readonly ICourseTypeRepository _courseTypeRepository;

        public CourseTypeLogic(ICourseTypeRepository courseTypeRepository)
        {
            _courseTypeRepository = courseTypeRepository;
        }

        public long SetCourseType(CourseTypeDTO model)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("Description", model.Description);
            parameters.Add("CourseCode", model.CourseCode);
            return _courseTypeRepository.SetCourseType(parameters);
        }

        public List<CourseTypeDTO> GetCourseTypes(CourseTypeDTO model)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("Id", model.Id);
            parameters.Add("CourseCode", model.CourseCode);
            parameters.Add("Description", model.Description);
            return _courseTypeRepository.GetCourseTypes(parameters);
        }
    }
}
