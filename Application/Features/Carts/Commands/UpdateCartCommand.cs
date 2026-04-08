using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Carts;
using MediatR;
using Shared;

namespace Application.Features.Carts.Commands;

public class UpdateCartCommand : IRequest<Result<string>>
{
    public int Id { get; set; }
    public CreateCartCommand CreateCart { get; set; }
    public UpdateCartCommand(int id, CreateCartCommand createCart)
    {
        Id = id;
        CreateCart = createCart;
    }
}
internal class UpdateCartCommandHandler : IRequestHandler<UpdateCartCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCartCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
    {
        if (request.CreateCart.TableId != 0)
        {
            var table = await _unitOfWork.Repository<Domain.Entities.Tables.Table>().GetByIdAsync(request.CreateCart.TableId);
            if (table == null)
            {
                return Result<string>.BadRequest("Table Id is not exist.");
            }
        }
        var cart = await _unitOfWork.Repository<Cart>().GetByIdAsync(request.Id);
        if (cart == null)
        {
            return Result<string>.BadRequest("Cart Not Found");
        }
        _mapper.Map(request.CreateCart, cart);
        await _unitOfWork.Repository<Cart>().UpdateAsync(cart);
        await _unitOfWork.Save(cancellationToken);
        return Result<string>.Success("Cart Updated Succesfully");
    }
}
