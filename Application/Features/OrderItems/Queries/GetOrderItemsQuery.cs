using Application.Dtos.OrderItems;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Orders;
using MediatR;
using Shared;

namespace Application.Features.OrderItems.Queries;

public class GetOrderItemsQuery : IRequest<Result<List<GetOrderItemDto>>>
{
}
internal class GetOrderItemsQueryHandler : IRequestHandler<GetOrderItemsQuery, Result<List<GetOrderItemDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetOrderItemsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<GetOrderItemDto>>> Handle(GetOrderItemsQuery request, CancellationToken cancellationToken)
    {

        var orderItems = await _unitOfWork.Repository<OrderItem>().GetAllAsync();
        var result = _mapper.Map<List<GetOrderItemDto>>(orderItems);
        return Result<List<GetOrderItemDto>>.Success(result, "OrderItems");
    }
}
