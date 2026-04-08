using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Commons.Interfaces;

public class IBaseAuditableEntity
{
    int Id { get; set; }
    DateTime? CreatedDate { get; set; }
    DateTime? UpdatedDate { get; set; }
    int? CreatedBy { get; set; }
    int? UpdatedBy { get; set; }
    bool IsDeleted { get; set; }
    bool IsActive { get; set; }
}
