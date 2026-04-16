using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.BillItems;
using Domain.Entities.Bills;
using MediatR;
using Shared;

namespace Application.Features.BillItems.Commands;

public class UpdateBillItemCommand : IRequest<Result<string>>
{
    public int Id { get; set; }
    public CreateBillItemCommand CreateBillItem { get; set; }
    public UpdateBillItemCommand(int id, CreateBillItemCommand createBillItem)
    {
        Id = id;
        CreateBillItem = createBillItem;
    }
}
internal class UpdateBillItemCommandHandler : IRequestHandler<UpdateBillItemCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateBillItemCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(UpdateBillItemCommand request, CancellationToken cancellationToken)
    {
        if (request.CreateBillItem.ProductId.HasValue)
        {
            var product = await _unitOfWork.Repository<Domain.Entities.Products.Product>().GetByIdAsync(request.CreateBillItem.ProductId.Value);
            if (product == null)
            {
                return Result<string>.BadRequest("Product Id is not exist.");
            }
        }

        if (request.CreateBillItem.BillId.HasValue)
        {
            var bill = await _unitOfWork.Repository<Bill>().GetByIdAsync(request.CreateBillItem.BillId.Value);
            if (bill == null)
            {
                return Result<string>.BadRequest("Bill Id is not exist.");
            }
        }

        var billItem = await _unitOfWork.Repository<BillItem>().GetByIdAsync(request.Id);
        if (billItem == null)
        {
            return Result<string>.BadRequest("BillItem Not Found");
        }
        _mapper.Map(request.CreateBillItem, billItem);
        await _unitOfWork.Repository<BillItem>().UpdateAsync(billItem);
        await _unitOfWork.Save(cancellationToken);
        return Result<string>.Success("BillItem Updated Succesfully");
    }
}
