using Application.Dtos.CartItems;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.CartItems;
using MediatR;
using Shared;

namespace Application.Features.CartItems.Queries;

public class GetCartItemsQuery : IRequest<Result<List<GetCartItemDto>>>
{
}
internal class GetCartItemsQueryHandler : IRequestHandler<GetCartItemsQuery, Result<List<GetCartItemDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCartItemsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<GetCartItemDto>>> Handle(GetCartItemsQuery request, CancellationToken cancellationToken)
    {
        var cartItems = await _unitOfWork.Repository<CartItem>().GetAllAsync();
        var result = _mapper.Map<List<GetCartItemDto>>(cartItems);
        return Result<List<GetCartItemDto>>.Success(result, "CartItems");
    }
}
