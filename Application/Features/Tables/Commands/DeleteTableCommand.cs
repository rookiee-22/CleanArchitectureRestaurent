using Application.Interfaces.Repositories;
using Domain.Entities.Tables;
using MediatR;
using Shared;

namespace Application.Features.Tables.Commands;

public class DeleteTableCommand : IRequest<Result<string>>
{
    public int Id { get; set; }
    public DeleteTableCommand(int id)
    {
        Id = id;
    }
}
internal class DeleteTableCommandHandler : IRequestHandler<DeleteTableCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTableCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(DeleteTableCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.Repository<Table>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);
        return Result<string>.Success("Table Deleted Succesfully.");
    }
}
