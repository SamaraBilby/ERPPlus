using ERPPlus.Domain.Interfaces.Repositories;
using ERPPlus.Infrastructure.Persistence;
using ERPPlus.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace ERPPlus.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ERPPlusDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Postgres")));

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            
            
            return services;
        }
    }
}
