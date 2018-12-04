using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Section : BaseModel
    {
        public string Name { get; set; }
        public int Strength { get; set; }
        public Guid GradeId { get; set; }
        public Grade Grade { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
