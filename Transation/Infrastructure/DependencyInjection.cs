using Microsoft.Extensions.DependencyInjection;
using System;
using Core;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<AppDbContext>(c =>
            //    c.UseSqlServer(configuration.GetConnectionString("TransationDB")), b => b.);

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("TransationDB"),
                b => b.MigrationsAssembly("Infrastructure")));


            return services;
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {

            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ITransactionLogRepository, TransactionLogRepository>();


            return services;
        }
    }
}
