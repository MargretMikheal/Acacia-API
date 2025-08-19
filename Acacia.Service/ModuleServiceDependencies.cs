using Microsoft.Extensions.DependencyInjection;

namespace Acacia.Service
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            return services;
        }
    }
}
