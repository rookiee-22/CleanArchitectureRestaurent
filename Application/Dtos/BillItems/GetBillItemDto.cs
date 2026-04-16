using Application.Commons.Mappings.Commons;
using Application.Dtos.Commons;
using Application.Dtos.Products;
using AutoMapper;
using Domain.Entities.BillItems;

namespace Application.Dtos.BillItems;

public class GetBillItemDto : CommonDto, IMapFrom<BillItem>
{
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public decimal Total => Price * Quantity;
    public GetProductDto Product { get; set; }
    public int BillId { get; set; }
  


}
