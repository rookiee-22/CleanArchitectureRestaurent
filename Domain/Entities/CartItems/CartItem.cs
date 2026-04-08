using Domain.Commons;
using Domain.Entities.Carts;
using Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities.CartItems;

public class CartItem : BaseAuditableEntity
{
    [ForeignKey("Cart")]
    public int CartId { get; set; }
    public Cart Cart { get; set; }
    [ForeignKey("Product")]
    public int ProductId { get; set; }
    public Product Product { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal TotalPrice => UnitPrice * Quantity;
}
