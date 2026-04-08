using Application.Commons.Mappings.Commons;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Orders;
using MediatR;
using Shared;

namespace Application.Features.OrderItems.Commands;

public class CreateOrderItemCommand : IRequest<Result<int>>, ICreateMapFrom<OrderItem>
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public int UnitPrice { get; set; }
}
internal class CreateOrderItemCommandHandler : IRequestHandler<CreateOrderItemCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateOrderItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.Repository<Order>().GetByIdAsync(request.OrderId);
        if (order == null)
        {
            return Result<int>.BadRequest("Order id not exist");
        }

        var product = await _unitOfWork.Repository<Domain.Entities.Products.Product>().GetByIdAsync(request.ProductId);
        if (product == null)
        {
            return Result<int>.BadRequest("Product id not exist");
        }

        var orderItem = _mapper.Map<OrderItem>(request);
        await _unitOfWork.Repository<OrderItem>().CreateAsync(orderItem);
        await _unitOfWork.Save(cancellationToken);
        return Result<int>.Success(orderItem.Id, "OrderItem Created Successfully");
    }
}
