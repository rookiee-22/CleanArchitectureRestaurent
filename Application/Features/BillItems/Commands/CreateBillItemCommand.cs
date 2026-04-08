using Application.Commons.Mappings.Commons;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.BillItems;
using Domain.Entities.Bills;
using Domain.Entities.Products;
using MediatR;
using Shared;

namespace Application.Features.BillItems.Commands;

public class CreateBillItemCommand : IRequest<Result<int>>, ICreateMapFrom<BillItem>
{
    public int? ProductId { get; set; }
    public int? BillId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
internal class CreateBillItemCommandHandler : IRequestHandler<CreateBillItemCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateBillItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(CreateBillItemCommand request, CancellationToken cancellationToken)
    {
        if (request.ProductId.HasValue)
        {
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(request.ProductId.Value);
            if (product == null)
            {
                return Result<int>.BadRequest("Product id not exist");
            }
        }

        if (request.BillId.HasValue)
        {
            var bill = await _unitOfWork.Repository<Bill>().GetByIdAsync(request.BillId.Value);
            if (bill == null)
            {
                return Result<int>.BadRequest("Bill id not exist");
            }
        }

        var billItem = _mapper.Map<BillItem>(request);
        await _unitOfWork.Repository<BillItem>().CreateAsync(billItem);
        await _unitOfWork.Save(cancellationToken);
        return Result<int>.Success(billItem.Id, "BillItem Created Successfully");
    }
}
