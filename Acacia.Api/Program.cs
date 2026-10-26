using Acacia.Api.Health;
using Acacia.Core;
using Acacia.Core.MiddleWare;
using Acacia.Identity;
using Acacia.Identity.Models;
using Acacia.Identity.Seeding;
using Acacia.Infrastructure;
using Acacia.Infrastructure.Context;
using Acacia.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace Acacia.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddOpenApi();

        #region Health Checks & Observability
        builder.Services.AddHealthChecks()
            .AddCheck<DatabaseHealthCheck>("Database_SQLServer", tags: new[] { "ready", "db" });
        #endregion

        #region Dependency Injection
        builder.Services.AddInfrastructureDependencies()
            .AddServiceDependencies(builder.Configuration)
            .AddCoreDependencies()
            .AddIdentityServices(builder.Configuration)
            .AddServiceRegisteration(builder.Configuration);
        #endregion

        #region Localizations
        builder.Services.AddControllersWithViews();
        builder.Services.AddLocalization(opt =>
        {
            opt.ResourcesPath = "";
        });

        builder.Services.Configure<RequestLocalizationOptions>(options =>
        {
            List<CultureInfo> supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en-US"),
                new CultureInfo("de-DE"),
                new CultureInfo("fr-FR"),
                new CultureInfo("en-GB"),
                new CultureInfo("ar-EG"),
            };

            options.DefaultRequestCulture = new RequestCulture("en-US");
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
        });
        #endregion

        #region CORS Hardening
        var corsPolicyName = "_acaciaCorsPolicy";
        var configuredOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
            ?? new[] { "http://localhost:4200" };

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: corsPolicyName, policy =>
            {
                policy.WithOrigins(configuredOrigins)
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials()
                      .SetIsOriginAllowed(origin =>
                      {
                          // Allow localhost and MonsterASP subdomains (.runasp.net, .tryasp.net, .monsterasp.net)
                          var host = new Uri(origin).Host;
                          return host == "localhost"
                              || host.EndsWith(".monsterasp.net")
                              || host.EndsWith(".runasp.net")
                              || host.EndsWith(".tryasp.net");
                      });
            });
        });
        #endregion

        builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        builder.Services.AddTransient<IUrlHelper>(x =>
        {
            var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
            var factory = x.GetRequiredService<IUrlHelperFactory>();
            return factory.GetUrlHelper(actionContext!);
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Acacia API v1");
            c.RoutePrefix = "swagger";
        });

        #region Localization Middleware
        var localizationOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
        if (localizationOptions?.Value != null)
        {
            app.UseRequestLocalization(localizationOptions.Value);
        }
        #endregion

        app.UseMiddleware<ErrorHandlerMiddleware>();
        app.UseHttpsRedirection();
        app.UseCors(corsPolicyName);
        app.UseStaticFiles();

        // CRITICAL: Authentication must execute BEFORE Authorization
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapHealthChecks("/health");
        app.MapGet("/", () => Results.Redirect("/swagger"));
        app.MapControllers();

        // Seed default administrator and roles on startup
        try
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            await DataSeeder.SeedAsync(userManager, roleManager);
        }
        catch (Exception ex)
        {
            var logger = app.Services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred during database seeding.");
        }

        app.Run();
    }
}
