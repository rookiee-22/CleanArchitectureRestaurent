using Domain.Commons.Interfaces;

namespace Domain.Commons;

public class BaseAuditableEntity : IBaseAuditableEntity
{
    public int Id { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsActive { get; set; }
}
