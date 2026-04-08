using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.CartItems;
using MediatR;
using Shared;

namespace Application.Features.CartItems.Commands;

public class UpdateCartItemCommand : IRequest<Result<string>>
{
    public int Id { get; set; }
    public CreateCartItemCommand CreateCartItem { get; set; }
    public UpdateCartItemCommand(int id, CreateCartItemCommand createCartItem)
    {
        Id = id;
        CreateCartItem = createCartItem;
    }
}
internal class UpdateCartItemCommandHandler : IRequestHandler<UpdateCartItemCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCartItemCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
    {
        if (request.CreateCartItem.CartId != 0)
        {
            var cart = await _unitOfWork.Repository<Domain.Entities.Carts.Cart>().GetByIdAsync(request.CreateCartItem.CartId);
            if (cart == null)
            {
                return Result<string>.BadRequest("Cart Id is not exist.");
            }
        }

        if (request.CreateCartItem.ProductId != 0)
        {
            var product = await _unitOfWork.Repository<Domain.Entities.Products.Product>().GetByIdAsync(request.CreateCartItem.ProductId);
            if (product == null)
            {
                return Result<string>.BadRequest("Product Id is not exist.");
            }
        }

        var cartItem = await _unitOfWork.Repository<CartItem>().GetByIdAsync(request.Id);
        if (cartItem == null)
        {
            return Result<string>.BadRequest("CartItem Not Found");
        }
        _mapper.Map(request.CreateCartItem, cartItem);
        await _unitOfWork.Repository<CartItem>().UpdateAsync(cartItem);
        await _unitOfWork.Save(cancellationToken);
        return Result<string>.Success("CartItem Updated Succesfully");
    }
}
