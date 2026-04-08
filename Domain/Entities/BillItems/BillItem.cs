using Domain.Commons;
using Domain.Entities.Bills;
using Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities.BillItems;

public class BillItem : BaseAuditableEntity
{
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public decimal Total => Price * Quantity;

    [ForeignKey("Product")]
    public int? ProductId { get; set; }
    public Product Product { get; set; }
    [ForeignKey("Bill")]
    public int? BillId { get; set; }
    public Bill Bill { get; set; }
}
