using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Orders;
using MediatR;
using Shared;

namespace Application.Features.Orders.Commands;

public class UpdateOrderCommand : IRequest<Result<string>>
{
    public int Id { get; set; }
    public CreateOrderCommand CreateOrder { get; set; }
    public UpdateOrderCommand(int id, CreateOrderCommand createOrder)
    {
        Id = id;
        CreateOrder = createOrder;
    }
}
internal class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateOrderCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        if (request.CreateOrder.TableId != 0)
        {
            var table = await _unitOfWork.Repository<Domain.Entities.Tables.Table>().GetByIdAsync(request.CreateOrder.TableId);
            if (table == null)
            {
                return Result<string>.BadRequest("Table Id is not exist.");
            }
        }

        var order = await _unitOfWork.Repository<Order>().GetByIdAsync(request.Id);
        if (order == null)
        {
            return Result<string>.BadRequest("Order Not Found");
        }
        _mapper.Map(request.CreateOrder, order);
        await _unitOfWork.Repository<Order>().UpdateAsync(order);
        await _unitOfWork.Save(cancellationToken);
        return Result<string>.Success("Order Updated Succesfully");
    }
}
