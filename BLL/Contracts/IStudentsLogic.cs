using Common.Shared.DTO;
using System.Collections.Generic;

namespace BLL.Contracts
{
    public interface IStudentsLogic
    {
        long SetStudents(StudentsDTO model);
        List<StudentsDTO> GetStudents(StudentsDTO model);
    }
}
