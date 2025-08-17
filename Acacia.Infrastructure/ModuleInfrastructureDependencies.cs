using Acacia.Core.Interfaces.IReposetories;
using Acacia.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Acacia.Infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
