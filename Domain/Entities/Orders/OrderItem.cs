using Domain.Commons;
using Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities.Orders;

public class OrderItem:BaseAuditableEntity
{
    [ForeignKey("Order")]
 
    public int OrderId { get; set; }
    public Order Order { get; set; }
    [ForeignKey("Product")]

    public int ProductId { get; set; }
    public Product Product { get; set; }

    public int Quantity { get; set; }
    public int UnitPrice { get; set; }
}
