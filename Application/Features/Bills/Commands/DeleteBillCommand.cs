using Application.Interfaces.Repositories;
using Domain.Entities.Bills;
using MediatR;
using Shared;

namespace Application.Features.Bills.Commands;

public class DeleteBillCommand : IRequest<Result<string>>
{
    public int Id { get; set; }
    public DeleteBillCommand(int id)
    {
        Id = id;
    }
}
internal class DeleteBillCommandHandler : IRequestHandler<DeleteBillCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBillCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(DeleteBillCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.Repository<Bill>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);
        return Result<string>.Success("Bill Deleted Succesfully.");
    }
}
