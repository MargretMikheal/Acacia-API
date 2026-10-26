using Acacia.Core.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics;

namespace Acacia.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class HealthController : ControllerBase
    {
        private readonly HealthCheckService _healthCheckService;
        private static readonly DateTime _startTimeUtc = DateTime.UtcNow;

        public HealthController(HealthCheckService healthCheckService)
        {
            _healthCheckService = healthCheckService;
        }

        [HttpGet]
        public async Task<IActionResult> GetHealth(CancellationToken cancellationToken)
        {
            var sw = Stopwatch.StartNew();
            var report = await _healthCheckService.CheckHealthAsync(cancellationToken);
            sw.Stop();

            var healthSummary = new
            {
                Status = report.Status.ToString(),
                TotalDurationMs = Math.Round(sw.Elapsed.TotalMilliseconds, 2),
                UptimeSeconds = Math.Round((DateTime.UtcNow - _startTimeUtc).TotalSeconds, 0),
                TimestampUtc = DateTime.UtcNow,
                Checks = report.Entries.Select(e => new
                {
                    Name = e.Key,
                    Status = e.Value.Status.ToString(),
                    Description = e.Value.Description,
                    DurationMs = Math.Round(e.Value.Duration.TotalMilliseconds, 2),
                    Error = e.Value.Exception?.Message
                })
            };

            return report.Status == HealthStatus.Healthy
                ? Ok(new Response<object>(healthSummary, "System is fully operational."))
                : StatusCode(503, new Response<object>
                {
                    Succeeded = false,
                    Data = healthSummary,
                    Message = "System is degraded or unhealthy.",
                    Response_Code = System.Net.HttpStatusCode.ServiceUnavailable
                });
        }
    }
}
