using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Grade : BaseModel
    {
        public string Name { get; set; }
        public Guid SchoolId { get; set; }
        public School School { get; set; }
        public ICollection<Section> Sections { get; set; }
        public ICollection<CourseGrade> CourseGrades { get; set; }
    }
}
