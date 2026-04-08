using Application.Commons.Mappings.Commons;
using Application.Dtos.Commons;
using Domain.Entities.CartItems;

namespace Application.Dtos.CartItems;

public class GetCartItemDto : CommonDto, IMapFrom<CartItem>
{
    public int CartId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice => UnitPrice * Quantity;
}


