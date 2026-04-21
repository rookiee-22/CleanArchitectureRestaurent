using Application.Dtos.Users;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Users.Queries;

public class GetUsersQuery : IRequest<Result<List<GetUserDto>>>
{

}
internal class GetUserQueryHandler : IRequestHandler<GetUsersQuery, Result<List<GetUserDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<GetUserDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users= await _unitOfWork.Repository<User>().Entities.Where(x=>x.IsDeleted!=true).ToListAsync();
        var result = _mapper.Map<List<GetUserDto>>(users);
        return Result<List<GetUserDto>>.Success(result, "Users");
    }
}
