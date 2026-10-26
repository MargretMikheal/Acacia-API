using Acacia.Infrastructure.Context;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Acacia.Api.Health
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        private readonly AcaciaDbContext _context;

        public DatabaseHealthCheck(AcaciaDbContext context)
        {
            _context = context;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var canConnect = await _context.Database.CanConnectAsync(cancellationToken);
                return canConnect
                    ? HealthCheckResult.Healthy("Database connection is healthy.")
                    : HealthCheckResult.Unhealthy("Cannot connect to MS SQL database.");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("Database connection failed with exception.", ex);
            }
        }
    }
}
