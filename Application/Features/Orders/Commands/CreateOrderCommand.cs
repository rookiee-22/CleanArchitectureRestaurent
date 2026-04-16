using Application.Commons.Mappings.Commons;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Commons.Enums.Orders;
using Domain.Entities.Orders;
using MediatR;
using Shared;

namespace Application.Features.Orders.Commands;

public class CreateOrderCommand : IRequest<Result<int>>, ICreateMapFrom<Order>
{
    public int TableId { get; set; }
    public string Note { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
}
internal class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var table = await _unitOfWork.Repository<Domain.Entities.Tables.Table>().GetByIdAsync(request.TableId);
        if (table == null)
        {
            return Result<int>.BadRequest("Table id not exist");
        }

        var order = _mapper.Map<Order>(request);
        await _unitOfWork.Repository<Order>().CreateAsync(order);
        await _unitOfWork.Save(cancellationToken);
        return Result<int>.Success(order.Id, "Order Created Successfully");
    }
}
