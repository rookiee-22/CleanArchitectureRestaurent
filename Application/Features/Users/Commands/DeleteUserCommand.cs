using Application.Interfaces.Repositories;
using Domain.Entities.Users;
using MediatR;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Users.Commands;

public class DeleteUserCommand:IRequest<Result<string>>
{
    public int Id { get; set; }
    public DeleteUserCommand(int id)
    {
        Id = id;
    }
}
internal class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    public DeleteUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.Repository<User>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);
        return Result<string>.Success("User deleted Succesfully");
    }
}
