using Domain.Commons;
using Domain.Entities.CartItems;
using Domain.Entities.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities.Carts;

public class Cart : BaseAuditableEntity
{
    [ForeignKey("Table")]
    public int TableId { get; set; }
    public Table Table { get; set; }
    public List<CartItem> Items { get; set; } = new List<CartItem>();
    public int TotalAmount { get; set; }
    public string Note { get; set; } = null;
}
