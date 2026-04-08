using Application.Interfaces.Repositories;
using Domain.Entities.Otps;
using Domain.Entities.Users;
using MediatR;
using Shared;

namespace Application.Features.Auth.Command;

public class ForgotPasswordCommand : IRequest<Result<string>>
{
    public string Email { get; set; }
}
internal class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Result<string>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;
    public ForgotPasswordCommandHandler(IUnitOfWork unitOfWork, IEmailService emailService)
    {
        _unitOfWork = unitOfWork;
        _emailService = emailService;
    }

    public async Task<Result<string>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.Repository<User>().GetAllAsync();
        var user = users.FirstOrDefault(x => x.Email == request.Email);

        if (user == null)
            return Result<string>.BadRequest("User not found");


        int otpCode = GenerateOtp();
        var otp = new Otp
        {
            UserId = user.Id,
            Code = otpCode,
            CreateDate = DateTime.UtcNow
        };

        await _unitOfWork.Repository<Otp>().CreateAsync(otp);
        await _unitOfWork.Save(cancellationToken);

        await _emailService.SendEmail(request.Email, "Reset Password", $"Your OTP is {otpCode}");

        return Result<string>.Success("OTP sent successfully.");
    }

    private int GenerateOtp()
    {
        var random = new Random();
        return random.Next(1000, 9999);
    }
}
