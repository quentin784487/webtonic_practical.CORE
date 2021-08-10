using System.Data.Common;

namespace DAL.DataModel
{
    public class CourseType : Model
    {
        public string Description { get; set; }
        public string CourseCode { get; set; }

        public override void Populate(DbDataReader reader)
        {
            Id = GetLong(reader, "Id", -1);
            Description = GetString(reader, "Description", null);
            CourseCode = GetString(reader, "CourseCode", null);
        }
    }
}
