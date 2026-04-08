using Application.Commons.Mappings.Commons;
using Application.Dtos.Commons;
using Domain.Entities.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Categories;

public class GetCategoryDto : CommonDto,IMapFrom<Category>
{
    public string Name { get; set; }
    public string Profile { get; set; }
    public string Description { get; set; }
}
