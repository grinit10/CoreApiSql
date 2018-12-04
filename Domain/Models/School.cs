using System.Collections.Generic;

namespace Domain.Models
{
    public class School : BaseModel
    {
        public string Name { get; set; }
        public ICollection<Grade> Grades { get; set; }
    }
}
