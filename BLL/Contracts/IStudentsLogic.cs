using Common.Shared.DTO;
using System.Collections.Generic;

namespace BLL.Contracts
{
    public interface IStudentsLogic
    {
        long SetStudents(StudentsDTO model);
        PagedStudentsDTO GetStudents(StudentsDTO model, int pageIndex, int pageSize);
        void UpdateStudents(StudentsDTO model);
        void DeleteStudents(StudentsDTO model);
    }
}
