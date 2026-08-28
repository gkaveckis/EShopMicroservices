using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration) 
        {
             var connectionString = configuration.GetConnectionString("Database");
            //services.AddDbContext<OrderingContext>(options =>
            //{
            //    options.UseSqlServer(connectionString);
            //});
            //return services;

            return services;
        }
    }
}
