using Application.Interfaces.Repositories;
using Domain.Entities.BillItems;
using MediatR;
using Shared;

namespace Application.Features.BillItems.Commands;

public class DeleteBillItemCommand : IRequest<Result<string>>
{
    public int Id { get; set; }
    public DeleteBillItemCommand(int id)
    {
        Id = id;
    }
}
internal class DeleteBillItemCommandHandler : IRequestHandler<DeleteBillItemCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBillItemCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(DeleteBillItemCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.Repository<BillItem>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);
        return Result<string>.Success("BillItem Deleted Succesfully.");
    }
}
