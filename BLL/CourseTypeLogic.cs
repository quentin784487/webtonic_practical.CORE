using BLL.Contracts;
using Common.Shared.DTO;
using DAL.DataModel;
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

        public CourseTypeDTO GetCourseType(CourseTypeDTO model)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("Id", model.Id);
            parameters.Add("CourseCode", model.CourseCode);
            parameters.Add("Description", model.Description);

            PageResult courseResult = _courseTypeRepository.GetCourseTypes(parameters);
            CourseTypeDTO returnModel = new CourseTypeDTO();

            foreach (var course in courseResult.listItems)
            {
                returnModel.Id = course.Id;
                returnModel.CourseCode = course.CourseCode;
                returnModel.Description = course.Description;
            }
            return returnModel;
        }

        public List<CourseTypeDTO> GetCourseTypes()
        {
            PageResult courseResult = _courseTypeRepository.GetCourseTypes();
            List<CourseTypeDTO> returnModel = new List<CourseTypeDTO>();

            foreach (var course in courseResult.listItems)
            {
                returnModel.Add(new CourseTypeDTO()
                {
                    Id = course.Id,
                    CourseCode = course.CourseCode,
                    Description = course.Description
                });                
            }
            return returnModel;
        }
    }
}
