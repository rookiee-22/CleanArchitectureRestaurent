using Application.Interfaces.Repositories;
using Domain.Entities.Otps;
using Domain.Entities.Users;
using MediatR;
using Shared;

namespace Application.Features.Auth.Command;

public class VerifyOtpCommand : IRequest<Result<string>>
{
    public string Email { get; set; }
    public int Code { get; set; }
   
}

internal class VerifyOtpCommandHandler : IRequestHandler<VerifyOtpCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenerateToken _generateToken;

    public VerifyOtpCommandHandler(IUnitOfWork unitOfWork, IGenerateToken generateToken)
    {
        _unitOfWork = unitOfWork;
        _generateToken = generateToken;
    }



    public async Task<Result<string>> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
    {
        var otps = await _unitOfWork.Repository<Otp>().GetAllAsync();
        var otp = otps.FirstOrDefault(o => o.Code == request.Code);
        if (otp == null)
        {
            return Result<string>.BadRequest("Invalid OTP code.");
        }

        if (otp.CreateDate.AddMinutes(5) < DateTime.UtcNow)
        {
            return Result<string>.BadRequest("OTP expired");
        }

        var users = await _unitOfWork.Repository<User>().GetAllAsync();

        var user = users.FirstOrDefault(u => u.Email == request.Email);

        if (user == null)
        {
            return Result<string>.BadRequest("User not found");
        }

      
        await _unitOfWork.Repository<User>().UpdateAsync(user);
        await _unitOfWork.Save(cancellationToken);
        return Result<string>.Success(_generateToken.GenerateToken(user.Email,user.Role), "OTP verified successfully.");
    }
}
