using Application.Commons.Mappings.Commons;
using Application.Dtos.Commons;
using Domain.Entities.BillItems;

namespace Application.Dtos.BillItems;

public class GetBillItemDto : CommonDto, IMapFrom<BillItem>
{
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public decimal Total => Price * Quantity;
    public int ProductId { get; set; }
    public int BillId { get; set; }


}
