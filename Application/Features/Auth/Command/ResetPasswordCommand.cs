using Application.Interfaces.Repositories;
using Domain.Entities.Users;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Application.Features.Auth.Command;

public class ResetPasswordCommand : IRequest<Result<string>>
{
    public string Token { get; set; }
    public string NewPassword { get; set; } = string.Empty;
}


internal class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result<string>>
{
   private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _config;
    public ResetPasswordCommandHandler(IUnitOfWork unitOfWork, IConfiguration config)
    {
        _unitOfWork = unitOfWork;
        _config = config;
    }
    private string ExtractEmailFromToken(string token)
    {
        try
        {
            var secretKey = _config["JwtSettings:SecretKey"]
                ?? throw new InvalidOperationException("JWT SecretKey is not configured.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = true,
                ValidIssuer = _config["JwtSettings:Issuer"],
                ValidateAudience = true,
                ValidAudience = _config["JwtSettings:Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var email = principal.FindFirst("username")?.Value;
            return email ?? throw new InvalidOperationException("Email claim not found in token.");
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Invalid token: {ex.Message}", ex);
        }
    }


    public async Task<Result<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var email = ExtractEmailFromToken(request.Token);
        var users = await _unitOfWork.Repository<User>().GetAllAsync();
        
        var user = users.FirstOrDefault(u => u.Email == email);
        
        if (user == null)
        {
            return Result<string>.BadRequest("User not found.");
        }
        
        user.Password = request.NewPassword;
        
        await  _unitOfWork.Repository<User>().UpdateAsync(user);
        await _unitOfWork.Save(cancellationToken);
       
        return Result<string>.Success("Password reset successfully.");
    }
}