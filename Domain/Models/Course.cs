using System.Collections.Generic;

namespace Domain.Models
{
    public class Course : BaseModel
    {
        public string Name { get; set; }
        public ICollection<CourseGrade> CourseGrades { get; set; }
        public ICollection<CourseStudent> CourseStudents { get; set; }
    }
}
