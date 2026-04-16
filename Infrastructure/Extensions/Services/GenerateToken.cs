using Application.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Extensions.Services;

public class GenerateToken1: IGenerateToken
{
    private readonly IConfiguration _config;

    public GenerateToken1(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateToken(string email)
    {
        var secretKey = _config["JwtSettings:SecretKey"]   
            ?? throw new InvalidOperationException("JWT SecretKey is not configured.");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var claims = new List<Claim>
    {
        new Claim("username", email)
    };

        var token = new JwtSecurityToken(
            issuer: _config["JwtSettings:Issuer"],         
            audience: _config["JwtSettings:Audience"],     
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
