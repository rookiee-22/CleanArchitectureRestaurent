using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Users;
using MediatR;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;


namespace Application.Features.Users.Commands;

public class UpdateUserCommand:IRequest<Result<string>>
{
    public UpdateUserCommand(int id,CreateUserCommand createUser)
    {
        Id = id;
        CreateUser= createUser;
    }
    public int Id { get; set; }
    public CreateUserCommand CreateUser { get; set; }
}
internal class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<string>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Result<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Repository<User>().GetByIdAsync(request.Id);
        if (user == null)
        {
            return Result<string>.BadRequest("User Not found");
        }
        _mapper.Map(request.CreateUser, user);
        await _unitOfWork.Repository<User>().UpdateAsync(user);
        await _unitOfWork.Save(cancellationToken);
        return Result<string>.Success("User updated succesfully");
    }
}
