using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Categories;

public class Category : BaseAuditableEntity
{
    public string Name { get; set; }
    public string Profile {  get; set; }
    public string Description { get; set; }
}
