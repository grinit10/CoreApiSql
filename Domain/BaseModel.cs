using System;

namespace Domain
{
    public class BaseModel : IBaseModel
    {
        public BaseModel()
        {
            Id = Guid.NewGuid();
            CreatedBy = Guid.NewGuid();
            CreatedDate = DateTime.Now;
        }
        public Guid Id { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
