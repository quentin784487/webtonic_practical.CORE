using BLL.Contracts;
using Common.Shared.DTO;
using DAL.DataModel;
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
            parameters.Add("CourseId", model.CourseId);
            parameters.Add("CreatedDate", DateTime.Now);
            parameters.Add("ModifiedDate", DateTime.Now);
            return _studentsRepository.SetStudents(parameters);
        }

        public PagedStudentsDTO GetStudents(StudentsDTO model, int pageIndex, int pageSize)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("StudentNumber", model.StudentNumber);
            parameters.Add("FirstName", model.FirstName);
            parameters.Add("Surname", model.Surname);
            parameters.Add("CourseId", model.CourseId);
            parameters.Add("pageIndex", pageIndex);
            parameters.Add("pageSize", pageSize);

            PageResult studentsResult = _studentsRepository.GetStudents(parameters).listItems;
            PagedStudentsDTO pagedModel = new PagedStudentsDTO(studentsResult.totalCount);

            foreach (Students student in studentsResult.listItems)
            {
                pagedModel.students.Add(new StudentsDTO() { 
                    Id = student.Id,
                    FirstName = student.FirstName,
                    Surname = student.Surname,
                    StudentNumber = student.StudentNumber,
                    CourseCode = student.CourseCode,
                    CourseDescription = student.CourseDescription
                });
            }
            return pagedModel;
        }

        public void UpdateStudents(StudentsDTO model)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("StudentNumber", model.StudentNumber);
            parameters.Add("FirstName", model.FirstName);
            parameters.Add("Surname", model.Surname);
            parameters.Add("CourseId", model.CourseId);
            _studentsRepository.UpdateStudents(parameters);
        }

        public void DeleteStudents(StudentsDTO model)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("Id", model.Id);
            _studentsRepository.DeleteStudents(parameters);
        }
    }
}
