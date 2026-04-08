using Application.Interfaces.Repositories;
using Domain.Entities.CartItems;
using MediatR;
using Shared;

namespace Application.Features.CartItems.Commands;

public class DeleteCartItemCommand : IRequest<Result<string>>
{
    public int Id { get; set; }
    public DeleteCartItemCommand(int id)
    {
        Id = id;
    }
}
internal class DeleteCartItemCommandHandler : IRequestHandler<DeleteCartItemCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCartItemCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.Repository<CartItem>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);
        return Result<string>.Success("CartItem Deleted Succesfully.");
    }
}
