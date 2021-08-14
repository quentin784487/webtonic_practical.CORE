using DAL.DataModel;
using System.Collections.Generic;

namespace Repository.Contracts
{
    public interface IStudentsRepository
    {
        long SetStudent(Dictionary<string, object> model);
        PageResult GetStudent(Dictionary<string, object> model);
        PageResult GetStudents(Dictionary<string, object> model);
        void UpdateStudent(Dictionary<string, object> model);
        void DeleteStudent(Dictionary<string, object> model);
    }
}
