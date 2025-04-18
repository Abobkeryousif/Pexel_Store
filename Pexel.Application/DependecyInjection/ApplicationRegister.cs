using Microsoft.Extensions.DependencyInjection;
using Pexel.Application.Mapping;
using Pexel.Application.Validtor;
using System.Reflection;


namespace Pexel.Application.DependecyInjection
{
    public static class ApplicationRegister
    {
        public static IServiceCollection AppRegister(this IServiceCollection services) 
        {
            services.AddAutoMapper(typeof(AutoMapping));
            services.AddMediatR(m=> m.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            
            return services;
        }
    }
}
