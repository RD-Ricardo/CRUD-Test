using Domain.Repositories;
using Infrasctucture.Database;
using Infrasctucture.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrasctucture
{
    public static class InfrasctuctureModule
    {
        public static IServiceCollection AddInfrasctucture(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IUnitOfWork, ApplicationDbContext>();

            return services;
        }

    }
}
