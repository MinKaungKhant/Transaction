using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class DepenencyInjection
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {

            services.AddScoped<ITransactionService, TransactionService>();

            services.AddScoped<ITransactionLogService, TransactionLogService>();

            return services;
        }
    }
}
