﻿using Application.Customers.Commands.CreateCustomer;
using Application.Mappers;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCustomerCommand).Assembly));

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<AddressProfile>();
            });

            services.AddValidatorsFromAssemblies(new[]
            {
                typeof(CreateCustomerCommand).Assembly
            });


            return services;
        }
    }
}
