using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;


namespace Application.Features.Auth.Command;

public class LoginCommand : IRequest<Result<string>>
{
    public string Email { get; set; }
    public string Password { get; set; }
}

internal class LoginCommandHandler : IRequestHandler<LoginCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenerateToken _generateToken;
    private readonly IMapper _mapper;

    public LoginCommandHandler(IUnitOfWork unitOfWork, IGenerateToken generateToken, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _generateToken = generateToken;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Repository<User>().Entities.Where(x => x.IsActive == true).FirstOrDefaultAsync(x => x.Email == request.Email && x.Password == request.Password);

        //var user = users.FirstOrDefault(x => x.Email == request.Email && x.Password == request.Password);
       
        if (user == null)
        {
            return Result<string>.BadRequest("User not found");
        }

        var token = _generateToken.GenerateToken(user.Email,user.Role);

        //var result = _mapper.Map<LoginDto>(user);
        return Result<string>.Success(token, "Login successful");
    }
}