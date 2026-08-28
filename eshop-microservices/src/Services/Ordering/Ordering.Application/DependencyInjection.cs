using BuildingBlocks.Behaviors;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application
{
    public static  class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Add application services here
            //services.Services.AddMediatR(cfg =>
            //{
            //    cfg.RegisterServicesFromAssembly(assembly);
            //    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            //    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            //});

            return services;
        }
    }
}
