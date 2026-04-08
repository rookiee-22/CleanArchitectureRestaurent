using Domain.Commons;
using Domain.Commons.Enums.Orders;
using Domain.Entities.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities.Orders;

public class Order : BaseAuditableEntity
{
    [ForeignKey("Table")]
    public int TableId { get; set; }
    public Table Table { get; set; }
    public string? Note { get; set; }

    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    public int TotalPrice { get; set; }
}
