using Common.Shared.DTO;
using System.Collections.Generic;

namespace BLL.Contracts
{
    public interface IStudentsLogic
    {
        int[] ImportStudents(List<StudentsDTO> model);
        PagedStudentsDTO GetStudents(StudentsDTO model, int pageIndex, int pageSize);
        StudentsDTO GetStudent(StudentsDTO model);
        void UpdateStudent(StudentsDTO model);
        void DeleteStudent(StudentsDTO model);
    }
}
