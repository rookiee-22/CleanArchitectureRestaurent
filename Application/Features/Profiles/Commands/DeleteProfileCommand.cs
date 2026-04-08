using Application.Interfaces.Repositories;
using Domain.Entities.Profiles;
using MediatR;
using Shared;
using DomainProfile = Domain.Entities.Profiles.Profile;

namespace Application.Features.Profiles.Commands;

public class DeleteProfileCommand : IRequest<Result<string>>
{
    public int Id { get; set; }
    public DeleteProfileCommand(int id)
    {
        Id = id;
    }
}
internal class DeleteProfileCommandHandler : IRequestHandler<DeleteProfileCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProfileCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(DeleteProfileCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.Repository<DomainProfile>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);
        return Result<string>.Success("Profile Deleted Succesfully.");
    }
}
