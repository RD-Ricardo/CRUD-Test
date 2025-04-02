using Domain.Repositories;
using Infrasctucture.Database.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrasctucture
{
    public static class InfrasctuctureModule
    {
        public static IServiceCollection AddInfrasctucture(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

    }
}
