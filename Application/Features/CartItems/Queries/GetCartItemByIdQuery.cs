using Application.Dtos.CartItems;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.CartItems;
using MediatR;
using Shared;

namespace Application.Features.CartItems.Queries;

public class GetCartItemByIdQuery : IRequest<Result<GetCartItemDto>>
{
    public int Id { get; set; }
    public GetCartItemByIdQuery(int id)
    {
        Id = id;
    }
}
internal class GetCartItemByIdQueryHandler : IRequestHandler<GetCartItemByIdQuery, Result<GetCartItemDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCartItemByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetCartItemDto>> Handle(GetCartItemByIdQuery request, CancellationToken cancellationToken)
    {
        var cartItem = await _unitOfWork.Repository<CartItem>().GetByIdAsync(request.Id);
        var result = _mapper.Map<GetCartItemDto>(cartItem);
        return Result<GetCartItemDto>.Success(result, "CartItem");
    }
}
