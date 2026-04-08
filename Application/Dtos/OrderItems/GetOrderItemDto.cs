using Application.Commons.Mappings.Commons;
using Application.Dtos.Commons;
using Domain.Entities.Orders;

namespace Application.Dtos.OrderItems;

public class GetOrderItemDto : CommonDto, IMapFrom<OrderItem>
{
    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }
    public int UnitPrice { get; set; }
}
