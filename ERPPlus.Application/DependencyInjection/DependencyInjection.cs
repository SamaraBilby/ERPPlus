using ERPPlus.Application.Services;
using ERPPlus.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;


namespace ERPPlus.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();

            return services;
        }
    }
}
