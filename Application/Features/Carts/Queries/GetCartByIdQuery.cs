using Application.Dtos.Carts;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Carts;
using MediatR;
using Shared;

namespace Application.Features.Carts.Queries;

public class GetCartByIdQuery : IRequest<Result<GetCartDto>>
{
    public int Id { get; set; }
    public GetCartByIdQuery(int id)
    {
        Id = id;
    }
}
internal class GetCartByIdQueryHandler : IRequestHandler<GetCartByIdQuery, Result<GetCartDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCartByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetCartDto>> Handle(GetCartByIdQuery request, CancellationToken cancellationToken)
    {
        var cart = await _unitOfWork.Repository<Cart>().GetByIdAsync(request.Id);
        var result = _mapper.Map<GetCartDto>(cart);
        return Result<GetCartDto>.Success(result, "Cart");
    }
}
