using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ViewModels
{
    public class PostStudentVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid SectionId { get; set; }
    }
}
