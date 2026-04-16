using Application.Commons.Mappings.Commons;
using Application.Dtos.Commons;
using Application.Dtos.Products;
using AutoMapper;
using Domain.Entities.Orders;

namespace Application.Dtos.OrderItems;

public class GetOrderItemDto : CommonDto, IMapFrom<OrderItem>
{
    public int OrderId { get; set; }
    public GetProductDto Product { get; set; }

    public int Quantity { get; set; }
    public int UnitPrice { get; set; }
    public int TotalPrice => Quantity * UnitPrice;
}
