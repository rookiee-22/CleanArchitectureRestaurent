using Application.Dtos.Orders;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Orders;
using MediatR;
using Shared;

namespace Application.Features.Orders.Queries;

public class GetOrdersQuery : IRequest<Result<List<GetOrderDto>>>
{
}
internal class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, Result<List<GetOrderDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetOrdersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<GetOrderDto>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _unitOfWork.Repository<Order>().GetAllAsync();
        var result = _mapper.Map<List<GetOrderDto>>(orders);
        return Result<List<GetOrderDto>>.Success(result, "Orders");
    }
}
