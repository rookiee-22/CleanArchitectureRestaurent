using Application.Interfaces.Repositories;
using Infrastructure.Extensions.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Extensions;

public static class ServiceExtensionCollection
{
    public static void AddInfrastructureLayer(this IServiceCollection services  )
    {
        services.AddService();
    }
    public static void AddService(this IServiceCollection services)
    {
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IGenerateToken, GenerateToken1>();
    }
}
