using Application.Interfaces.Repositories;
using Domain.Entities.Orders;
using MediatR;
using Shared;

namespace Application.Features.OrderItems.Commands;

public class DeleteOrderItemCommand : IRequest<Result<string>>
{
    public int Id { get; set; }
    public DeleteOrderItemCommand(int id)
    {
        Id = id;
    }
}
internal class DeleteOrderItemCommandHandler : IRequestHandler<DeleteOrderItemCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteOrderItemCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(DeleteOrderItemCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.Repository<OrderItem>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);
        return Result<string>.Success("OrderItem Deleted Succesfully.");
    }
}
