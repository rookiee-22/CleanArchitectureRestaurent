using Domain.Commons;
using Domain.Commons.Enums.BIlls;
using Domain.Entities.BillItems;
using Domain.Entities.Tables;
using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities.Bills;

public class Bill :BaseAuditableEntity
{
    public decimal TotalAmount { get; set; }
    public string? Note { get; set; }
    public BillStatus Status { get; set; } = BillStatus.Done;
    public ICollection<BillItem> BillItems { get; set; } = new List<BillItem>();
    [ForeignKey("Table")]
    public int TableId { get; set; }
    public Table Table { get; set; }
}
