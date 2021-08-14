namespace Common.Shared.DTO
{
    public class StudentsDTO
    {
        public long Id { get; set; }
        public int? StudentNumber { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string CourseDescription { get; set; }
        public string CourseCode { get; set; }
        public int? CourseId { get; set; }
        public string Grade { get; set; }
    }
}
