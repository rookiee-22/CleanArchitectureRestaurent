using Domain.Commons;
using Domain.Entities.Categories;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Products;

public class Product : BaseAuditableEntity
{
    public string Name { get; set; }
    public int Price { get; set; }
    public string Profile { get; set; }
    public string Description { get; set; }

    [ForeignKey("Category")]
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}
