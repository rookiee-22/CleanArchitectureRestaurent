using Application.Commons.Mappings.Commons;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Otps;
using Domain.Entities.Users;
using MediatR;
using Shared;


namespace Application.Features.Users.Commands;

public class CreateUserCommand : IRequest<Result<string>>, ICreateMapFrom<User>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public long MobileNo { get; set; }
    public string Password { get; set; }
    public int Role { get; set; }
}
internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;


    public CreateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
                
    }

    public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        request.Email = request.Email?.ToLower();

        if (string.IsNullOrEmpty(request.Email))
            return Result<string>.BadRequest("Email is required");

        var userRepo = _unitOfWork.Repository<User>();
        var existingUser = (await userRepo.GetAllAsync())
            .FirstOrDefault(u => u.Email == request.Email);

        if (existingUser != null)
            return Result<string>.BadRequest("User with the same email already exists");
        var user = _mapper.Map<User>(request);
        user.IsActive = true;
        await userRepo.CreateAsync(user);
        await _unitOfWork.Save(cancellationToken);
    
        return Result<string>.Success("User created successfully.");
    }
   
}
