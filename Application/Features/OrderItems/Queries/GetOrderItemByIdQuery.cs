using Application.Dtos.OrderItems;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Orders;
using MediatR;
using Shared;

namespace Application.Features.OrderItems.Queries;

public class GetOrderItemByIdQuery : IRequest<Result<GetOrderItemDto>>
{
    public int Id { get; set; }
    public GetOrderItemByIdQuery(int id)
    {
        Id = id;
    }
}
internal class GetOrderItemByIdQueryHandler : IRequestHandler<GetOrderItemByIdQuery, Result<GetOrderItemDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetOrderItemByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetOrderItemDto>> Handle(GetOrderItemByIdQuery request, CancellationToken cancellationToken)
    {
        var orderItem = await _unitOfWork.Repository<OrderItem>().GetByIdAsync(request.Id);
        var result = _mapper.Map<GetOrderItemDto>(orderItem);
        return Result<GetOrderItemDto>.Success(result, "OrderItem");
    }
}
