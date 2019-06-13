using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Student : BaseModel
    {
        public Student()
        {
            CourseStudents = new List<CourseStudent>();
        }
        public string FirstName {get; set;}
        public string LastName { get; set; }
        public Guid SectionId { get; set; }
        public Section Section { get; set; }
        public ICollection<CourseStudent> CourseStudents { get; }
    }
}
