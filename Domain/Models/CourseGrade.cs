using System;

namespace Domain.Models
{
    public class CourseGrade
    {
        public Course Course { get; set; }
        public Guid CourseId { get; set; }
        public Grade Grade { get; set; }
        public Guid GradeId { get; set; }
    }
}
