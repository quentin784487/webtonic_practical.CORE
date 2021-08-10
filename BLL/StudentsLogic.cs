using BLL.Contracts;
using Common.Shared.DTO;
using Repository.Contracts;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class StudentsLogic : IStudentsLogic
    {
        private readonly IStudentsRepository _studentsRepository;

        public StudentsLogic(IStudentsRepository studentsRepository)
        {
            _studentsRepository = studentsRepository;
        }

        public long SetStudents(StudentsDTO model)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("StudentNumber", model.StudentNumber);
            parameters.Add("FirstName", model.FirstName);
            parameters.Add("Surname", model.Surname);
            parameters.Add("CourseType", model.CourseType);
            parameters.Add("CreatedDate", DateTime.Now);
            parameters.Add("ModifiedDate", DateTime.Now);
            return _studentsRepository.SetStudents(parameters);
        }

        public List<StudentsDTO> GetStudents(StudentsDTO model)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("StudentNumber", model.StudentNumber);
            parameters.Add("FirstName", model.FirstName);
            parameters.Add("Surname", model.Surname);
            parameters.Add("CourseType", model.CourseType);
            return (List<StudentsDTO>)_studentsRepository.GetStudents(parameters).listItems;
        }
    }
}
