using Application.Interfaces.Repositories;
using Application.Interfaces.Respositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance.DataContexts;
using Persistance.Extensions.Repositories;

namespace Persistance.Extensions;

public static class ServiceExtenstionCollection
{
    public static void AddPersitenceLayer(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext(configuration);
        services.AddRepositories();
    }

    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    }
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    }
}
