using DAL.DataModel;
using System.Collections.Generic;

namespace Repository.Contracts
{
    public interface IStudentsRepository
    {
        long SetStudent(Dictionary<string, object> model);
        PageResult GetStudent(Dictionary<string, object> model);
        PageResult GetStudents(Dictionary<string, object> model);
        void UpdateStudents(Dictionary<string, object> model);
        void DeleteStudents(Dictionary<string, object> model);
    }
}
