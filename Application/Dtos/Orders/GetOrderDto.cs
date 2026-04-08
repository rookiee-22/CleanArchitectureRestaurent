using Application.Commons.Mappings.Commons;
using Application.Dtos.Commons;
using Domain.Commons.Enums.Orders;
using Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Orders;

public class GetOrderDto : CommonDto,IMapFrom<Order>
{
    public int TableId { get; set; }

    public string? Note { get; set; }

    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    public int TotalPrice { get; set; }
}
