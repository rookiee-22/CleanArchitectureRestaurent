using Application.Commons.Mappings.Commons;
using Application.Dtos.Commons;
using Domain.Commons.Enums.BIlls;
using Domain.Entities.BillItems;
using Domain.Entities.Bills;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Bills;

public class GetBillDto : CommonDto,IMapFrom<Bill>
{
    public decimal TotalAmount { get; set; }
    public string? Note { get; set; }
    public BillStatus Status { get; set; } = BillStatus.Preparing;
    public ICollection<BillItem> BillItems { get; set; } = new List<BillItem>();
    public int TableId { get; set; }
}
