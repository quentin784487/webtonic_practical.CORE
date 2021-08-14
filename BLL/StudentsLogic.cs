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
        private readonly ICourseTypeLogic _courseTypeLogic;

        public StudentsLogic(IStudentsRepository studentsRepository, ICourseTypeLogic courseTypeLogic)
        {
            _studentsRepository = studentsRepository;
            _courseTypeLogic = courseTypeLogic;
        }

        public int[] ImportStudents(List<StudentsDTO> model)
        {
            int[] returnResult = new int[2];
            foreach (var instance in model)
            {
                StudentsDTO student = this.GetStudent(instance);
                if (student.Id == 0)
                {
                    Dictionary<string, object> parameters = new Dictionary<string, object>();
                    parameters.Add("StudentNumber", instance.StudentNumber);
                    parameters.Add("FirstName", instance.FirstName);
                    parameters.Add("Surname", instance.Surname);
                    parameters.Add("CourseId", getStudentCourseKey(instance.CourseDescription, instance.CourseCode));
                    parameters.Add("Grade", instance.Grade);
                    parameters.Add("CreatedDate", DateTime.Now);
                    parameters.Add("ModifiedDate", DateTime.Now);
                    _studentsRepository.SetStudent(parameters);
                }                
            }            
            return returnResult;
        }

        private long getStudentCourseKey(string description, string code)
        {
            CourseTypeDTO model = new CourseTypeDTO();
            model.Description = description;
            CourseTypeDTO courseType = _courseTypeLogic.GetCourseType(model);
            if (courseType.Id > 0)
            {
                return courseType.Id;
            }
            else
            {
                model.Description = description;
                model.CourseCode = code;
                return _courseTypeLogic.SetCourseType(model);
            }
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

            PageResult studentsResult = _studentsRepository.GetStudents(parameters);
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

        public StudentsDTO GetStudent(StudentsDTO model)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("StudentNumber", model.StudentNumber);            
            parameters.Add("CourseCode", model.CourseCode);
            
            PageResult studentResult = _studentsRepository.GetStudent(parameters);
            PagedStudentsDTO returnModel = new PagedStudentsDTO();

            foreach (var student in studentResult.listItems)
            {
                returnModel.students.Add(new StudentsDTO() { 
                    Id = student.Id,                    
                    CourseId = student.CourseId,
                    FirstName = student.FirstName,
                    Surname = student.Surname,
                    StudentNumber = student.StudentNumber,
                    Grade = student.Grade
                });
                break;
            }

            if (returnModel.students.Count > 0)
            {
                return returnModel.students[0];
            }
            else
            {
                return new StudentsDTO();
            }
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
