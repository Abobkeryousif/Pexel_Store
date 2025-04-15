using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pexel.Application.Contracts.Interfaces;
using Pexel.Infrastructrue.Implementation;


namespace Pexel.Infrastructrue.DependencyInjection
{
    public static class InfrastructureRegister
    {
        public static IServiceCollection InfraRegister(this IServiceCollection services,IConfiguration configuration) 
        {
            services.AddDbContext<ApplicationDbContext>(o=> o.UseNpgsql(configuration.GetConnectionString("Default")));
            services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            return services;
        }
    }
}
