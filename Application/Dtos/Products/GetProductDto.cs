using Application.Commons.Mappings.Commons;
using Application.Dtos.Commons;
using Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Products;

public class GetProductDto : CommonDto,IMapFrom<Product>
{
    public string Name { get; set; }
    public int Price { get; set; }
    public string Profile { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }
}
