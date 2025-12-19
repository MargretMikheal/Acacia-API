using Acacia.Core.Interfaces.Services;
using Acacia.Core.Models.Cloudinary;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Acacia.Service
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // Bind Cloudinary settings
            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));

            // Register Cloudinary client
            services.AddScoped(provider =>
            {
                var settings = provider.GetRequiredService<IOptions<CloudinarySettings>>().Value;

                Account account = new Account(
                    settings.CloudName,
                    settings.ApiKey,
                    settings.ApiSecret
                );

                return new CloudinaryDotNet.Cloudinary(account);
            });

            // Register your service
            services.AddScoped<ICloudinaryService, CloudinaryService>();

            return services;
        }
    }
}
