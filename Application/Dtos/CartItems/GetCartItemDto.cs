using Application.Commons.Mappings.Commons;
using Application.Dtos.Commons;
using Application.Dtos.Products;
using Domain.Entities.CartItems;

namespace Application.Dtos.CartItems;

public class GetCartItemDto : CommonDto, IMapFrom<CartItem>
{
    public int CartId { get; set; }
    public GetProductDto Product { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice => UnitPrice * Quantity;
}


