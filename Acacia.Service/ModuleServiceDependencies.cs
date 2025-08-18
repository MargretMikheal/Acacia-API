using Acacia.Core.Interfaces.Services;
using Acacia.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Acacia.Service
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            //services.AddTransient<IProductSizeService, ProductSizeService>();
            return services;
        }
    }
}
