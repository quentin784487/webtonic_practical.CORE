using System.Collections.Generic;

namespace Common.Shared.DTO
{
    public class PagedStudentsDTO
    {
        public int totalCount { get; set; }
        public List<StudentsDTO> students { get; set; }
        public PagedStudentsDTO(int totalCount)
        {
            this.totalCount = totalCount;
            students = new List<StudentsDTO>();
        }
    }
}
