using Common.Shared.DTO;

namespace BLL.Contracts
{
    public interface ICourseTypeLogic
    {
        long SetCourseType(CourseTypeDTO model);
    }
}
