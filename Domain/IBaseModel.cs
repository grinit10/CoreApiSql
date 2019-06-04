using System;

namespace Domain
{
    internal interface IBaseModel
    {
        Guid Id { get; set; }
        Guid CreatedBy { get; set; }
        Guid? UpdatedBy { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime? UpdatedDate { get; set; }

    }
}
