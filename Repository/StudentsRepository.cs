using DAL.DataModel;
using DAL.DbContext;
using Repository.Contracts;
using System.Collections.Generic;

namespace Repository
{
    public class StudentsRepository : IStudentsRepository
    {
        private readonly IDataWrapper _dataWrapper;

        public StudentsRepository(IDataWrapper dataWrapper)
        {
            _dataWrapper = dataWrapper;
        }

        public long SetStudents(Dictionary<string, object> model)
        {
            return (long)_dataWrapper.ExecuteNonQuery("sp_setStudents", model);
        }

        public PageResult GetStudents(Dictionary<string, object> model)
        {
            return _dataWrapper.ExecuteDataReader<Students>("sp_getStudents", model, typeof(Students));
        }

        public void UpdateStudents(Dictionary<string, object> model)
        {
            _dataWrapper.ExecuteNonQuery("sp_updateStudents", model);
        }

        public void DeleteStudents(Dictionary<string, object> model)
        {
            _dataWrapper.ExecuteNonQuery("sp_deleteStudents", model);
        }
    }
}
