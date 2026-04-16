using Application.Commons.Mappings.Commons;
using Application.Dtos.CartItems;
using Application.Dtos.Commons;
using Domain.Entities.CartItems;
using Domain.Entities.Carts;
using Domain.Entities.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Carts;

public class GetCartDto : CommonDto,IMapFrom<Cart>
{
    public int TableId { get; set; }
    public List<GetCartItemDto> Items { get; set; } = new List<GetCartItemDto>();
    public int TotalAmount { get; set; }
    public string Note { get; set; } = null;
}
