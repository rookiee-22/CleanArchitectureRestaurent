using Application.Dtos.Users;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Users;
using MediatR;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Users.Queries;

public class GetUserByIdQuery:IRequest<Result<GetUserDto>>
{
    public int Id { get; set; }
    public GetUserByIdQuery(int id)
    {
        Id = id;
    }
}
internal class GetUserByQueryByIdHandler : IRequestHandler<GetUserByIdQuery, Result<GetUserDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserByQueryByIdHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetUserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Repository<User>().GetByIdAsync(request.Id);
        var result= _mapper.Map<GetUserDto>(user);
        return Result<GetUserDto>.Success(result, "User");
    }
}
