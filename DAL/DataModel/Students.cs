using System;
using System.Data.Common;

namespace DAL.DataModel
{
    public class Students : Model
    {
        public int StudentNumber { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public int CourseId { get; set; }
        public string CourseDescription { get; set; }
        public string CourseCode { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public override void Populate(DbDataReader reader)
        {
            Id = GetLong(reader, "Id", -1);
            StudentNumber = GetInt(reader, "StudentNumber", -1);
            FirstName = GetString(reader, "FirstName", null);
            Surname = GetString(reader, "Surname", null);
            CourseId = GetInt(reader, "Course", -1);
            CourseDescription = GetString(reader, "CourseDescription", null);
            CourseCode = GetString(reader, "CourseCode", null);
            CreatedDate = GetDateTime(reader, "CreatedDate");
            ModifiedDate = GetDateTime(reader, "ModifiedDate");
        }
    }
}
