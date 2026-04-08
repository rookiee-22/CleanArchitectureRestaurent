using Application.Interfaces.Repositories;
using Domain.Entities.Orders;
using MediatR;
using Shared;

namespace Application.Features.Orders.Commands;

public class DeleteOrderCommand : IRequest<Result<string>>
{
    public int Id { get; set; }
    public DeleteOrderCommand(int id)
    {
        Id = id;
    }
}
internal class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteOrderCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.Repository<Order>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);
        return Result<string>.Success("Order Deleted Succesfully.");
    }
}
