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
}
internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    private readonly IEmailService _emailService;
    public CreateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IGenerateToken generateToken, IEmailService emailService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
                _emailService = emailService;
    }

    public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        request.Email = request.Email?.ToLower();

        if (string.IsNullOrEmpty(request.Email))
            return Result<string>.BadRequest("Email is required");

        var userRepo = _unitOfWork.Repository<User>();
        var otpRepo = _unitOfWork.Repository<Otp>();


        var existingUser = (await userRepo.GetAllAsync())
            .FirstOrDefault(u => u.Email == request.Email);

        if (existingUser != null)
            return Result<string>.BadRequest("User with the same email already exists");


        var user = _mapper.Map<User>(request);
        user.IsDeleted = false;
        user.IsActive = false;

        await userRepo.CreateAsync(user);
        await _unitOfWork.Save(cancellationToken);


        if (user.Id <= 0)
            return Result<string>.BadRequest("User creation failed");


        int otpCode = GenerateOtp();

        var otp = new Otp
        {
            UserId = user.Id,
            Code = otpCode,
            CreateDate = DateTime.UtcNow
        };

        await otpRepo.CreateAsync(otp);
        await _unitOfWork.Save(cancellationToken);


        await _emailService.SendEmail(user.Email, "OTP Verification", $"Your OTP code is: {otp.Code}");

        return Result<string>.Success("User created successfully. Please verify OTP.");
    }
    private int GenerateOtp()
    {
        var random = new Random();
        return random.Next(1000, 9999);
    }
}
