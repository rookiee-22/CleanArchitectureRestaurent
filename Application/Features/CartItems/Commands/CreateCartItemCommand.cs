using Application.Commons.Mappings.Commons;
using Application.Dtos.Products;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.CartItems;
using MediatR;
using Shared;

namespace Application.Features.CartItems.Commands;

public class CreateCartItemCommand : IRequest<Result<int>>, ICreateMapFrom<CartItem>
{
    public int CartId { get; set; }
    public int  ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
internal class CreateCartItemCommandHandler : IRequestHandler<CreateCartItemCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateCartItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(CreateCartItemCommand request, CancellationToken cancellationToken)
    {
        var cart = await _unitOfWork.Repository<Domain.Entities.Carts.Cart>().GetByIdAsync(request.CartId);
        if (cart == null)
        {
            return Result<int>.BadRequest("Cart id not exist");
        }

        var product = await _unitOfWork.Repository<Domain.Entities.Products.Product>().GetByIdAsync(request.ProductId);
        if (product == null)
        {
            return Result<int>.BadRequest("Product id not exist");
        }

        var cartItem = _mapper.Map<CartItem>(request);
        await _unitOfWork.Repository<CartItem>().CreateAsync(cartItem);
        await _unitOfWork.Save(cancellationToken);
        return Result<int>.Success(cartItem.Id, "CartItem Created Successfully");
    }
}
