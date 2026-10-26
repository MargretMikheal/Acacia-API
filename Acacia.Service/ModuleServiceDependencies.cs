using Amazon.S3;
using Acacia.Core.Interfaces.Services;
using Acacia.Core.Models.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Acacia.Service;

public static class ModuleServiceDependencies
{
    public static IServiceCollection AddServiceDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        // Bind Backblaze B2 settings
        services.Configure<BackblazeSettings>(configuration.GetSection("Backblaze"));

        // Register Amazon S3 Client configured for Backblaze B2 S3-compatible API
        services.AddSingleton<IAmazonS3>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<BackblazeSettings>>().Value;

            var accessKey = string.IsNullOrWhiteSpace(settings.AccessKey) ? "dummy-key" : settings.AccessKey;
            var secretKey = string.IsNullOrWhiteSpace(settings.SecretKey) ? "dummy-secret" : settings.SecretKey;
            var region = string.IsNullOrWhiteSpace(settings.Region) ? "us-east-005" : settings.Region;
            var credentials = new Amazon.Runtime.BasicAWSCredentials(accessKey, secretKey);

            var config = new AmazonS3Config
            {
                ServiceURL = string.IsNullOrWhiteSpace(settings.Endpoint)
                    ? $"https://s3.{region}.backblazeb2.com"
                    : settings.Endpoint,
                AuthenticationRegion = region,
                ForcePathStyle = true
            };

            return new AmazonS3Client(credentials, config);
        });

        // Register Storage service (Backblaze B2)
        services.AddScoped<IStorageService, BackblazeStorageService>();
        services.AddScoped<ICloudinaryService, BackblazeStorageService>();

        return services;
    }
}
