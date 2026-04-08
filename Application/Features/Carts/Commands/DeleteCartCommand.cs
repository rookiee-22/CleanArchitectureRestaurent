using Application.Interfaces.Repositories;
using Domain.Entities.Carts;
using MediatR;
using Shared;

namespace Application.Features.Carts.Commands;

public class DeleteCartCommand : IRequest<Result<string>>
{
    public int Id { get; set; }
    public DeleteCartCommand(int id)
    {
        Id = id;
    }
}
internal class DeleteCartCommandHandler : IRequestHandler<DeleteCartCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCartCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.Repository<Cart>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);
        return Result<string>.Success("Cart Deleted Succesfully.");
    }
}
