using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Pexel.Application.DependecyInjection
{
    public static class ApplicationRegister
    {
        public static IServiceCollection AppRegister(this IServiceCollection services) 
        {
            services.AddMediatR(m=> m.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            return services;
        }
    }
}
