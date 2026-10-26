using Acacia.Api.Controllers;
using Acacia.Core.Bases;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Moq;
using Xunit;

namespace Acacia.Tests.Monitoring
{
    public class HealthCheckTests
    {
        [Fact]
        public async Task GetHealth_WhenHealthy_Returns200OkWithOperationalSummary()
        {
            // Arrange
            var mockHealthService = new Mock<HealthCheckService>();
            var entries = new Dictionary<string, HealthReportEntry>
            {
                ["Database_SQLServer"] = new HealthReportEntry(
                    HealthStatus.Healthy,
                    "Database connection is healthy.",
                    TimeSpan.FromMilliseconds(15),
                    null,
                    null)
            };
            var report = new HealthReport(entries, TimeSpan.FromMilliseconds(15));

            mockHealthService
                .Setup(s => s.CheckHealthAsync(It.IsAny<Func<HealthCheckRegistration, bool>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(report);

            var controller = new HealthController(mockHealthService.Object);

            // Act
            var result = await controller.GetHealth(CancellationToken.None);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(200);

            var response = okResult.Value as Response<object>;
            response.Should().NotBeNull();
            response!.Succeeded.Should().BeTrue();
            response.Message.Should().Contain("fully operational");
        }

        [Fact]
        public async Task GetHealth_WhenUnhealthy_Returns503ServiceUnavailable()
        {
            // Arrange
            var mockHealthService = new Mock<HealthCheckService>();
            var entries = new Dictionary<string, HealthReportEntry>
            {
                ["Database_SQLServer"] = new HealthReportEntry(
                    HealthStatus.Unhealthy,
                    "Cannot connect to MS SQL database.",
                    TimeSpan.FromMilliseconds(50),
                    new Exception("Connection timeout"),
                    null)
            };
            var report = new HealthReport(entries, TimeSpan.FromMilliseconds(50));

            mockHealthService
                .Setup(s => s.CheckHealthAsync(It.IsAny<Func<HealthCheckRegistration, bool>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(report);

            var controller = new HealthController(mockHealthService.Object);

            // Act
            var result = await controller.GetHealth(CancellationToken.None);

            // Assert
            var statusResult = result as ObjectResult;
            statusResult.Should().NotBeNull();
            statusResult!.StatusCode.Should().Be(503);

            var response = statusResult.Value as Response<object>;
            response.Should().NotBeNull();
            response!.Succeeded.Should().BeFalse();
            response.Message.Should().Contain("degraded or unhealthy");
        }
    }
}
