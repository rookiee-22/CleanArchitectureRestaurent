using Application.Dtos.Orders;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Orders;
using MediatR;
using Shared;

namespace Application.Features.Orders.Queries;

public class GetOrderByIdQuery : IRequest<Result<GetOrderDto>>
{
    public int Id { get; set; }
    public GetOrderByIdQuery(int id)
    {
        Id = id;
    }
}
internal class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Result<GetOrderDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetOrderByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetOrderDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.Repository<Order>().GetByIdAsync(request.Id);
        var result = _mapper.Map<GetOrderDto>(order);
        return Result<GetOrderDto>.Success(result, "Order");
    }
}
