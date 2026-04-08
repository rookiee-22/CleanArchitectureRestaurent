using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Orders;
using MediatR;
using Shared;

namespace Application.Features.OrderItems.Commands;

public class UpdateOrderItemCommand : IRequest<Result<string>>
{
    public int Id { get; set; }
    public CreateOrderItemCommand CreateOrderItem { get; set; }
    public UpdateOrderItemCommand(int id, CreateOrderItemCommand createOrderItem)
    {
        Id = id;
        CreateOrderItem = createOrderItem;
    }
}
internal class UpdateOrderItemCommandHandler : IRequestHandler<UpdateOrderItemCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateOrderItemCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(UpdateOrderItemCommand request, CancellationToken cancellationToken)
    {
        if (request.CreateOrderItem.OrderId != 0)
        {
            var order = await _unitOfWork.Repository<Order>().GetByIdAsync(request.CreateOrderItem.OrderId);
            if (order == null)
            {
                return Result<string>.BadRequest("Order Id is not exist.");
            }
        }

        if (request.CreateOrderItem.ProductId != 0)
        {
            var product = await _unitOfWork.Repository<Domain.Entities.Products.Product>().GetByIdAsync(request.CreateOrderItem.ProductId);
            if (product == null)
            {
                return Result<string>.BadRequest("Product Id is not exist.");
            }
        }

        var orderItem = await _unitOfWork.Repository<OrderItem>().GetByIdAsync(request.Id);
        if (orderItem == null)
        {
            return Result<string>.BadRequest("OrderItem Not Found");
        }
        _mapper.Map(request.CreateOrderItem, orderItem);
        await _unitOfWork.Repository<OrderItem>().UpdateAsync(orderItem);
        await _unitOfWork.Save(cancellationToken);
        return Result<string>.Success("OrderItem Updated Succesfully");
    }
}
