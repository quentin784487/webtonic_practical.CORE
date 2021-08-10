using DAL.DataModel;
using System.Collections.Generic;

namespace Repository.Contracts
{
    public interface IStudentsRepository
    {
        long SetStudents(Dictionary<string, object> model);
        PageResult GetStudents(Dictionary<string, object> model);
        void UpdateStudents(Dictionary<string, object> model);
        void DeleteStudents(Dictionary<string, object> model);
    }
}
