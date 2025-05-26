using ERPPlus.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPPlus.Infrastructure.Context
{
    public static class ERPPlusDbContextFactory
    {
        public static IServiceCollection AddERPPlusDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Postgres");
            services.AddDbContext<ERPPlusDbContext>(options  => options.UseNpgsql(connectionString));

            return services;
        }
    }
}
