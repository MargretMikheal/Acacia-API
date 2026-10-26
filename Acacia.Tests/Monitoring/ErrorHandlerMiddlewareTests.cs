using Acacia.Core.MiddleWare;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using System.IO;
using System.Text.Json;
using Xunit;

namespace Acacia.Tests.Monitoring
{
    public class ErrorHandlerMiddlewareTests
    {
        [Fact]
        public async Task Middleware_SetsCorrelationIdHeader_OnEveryRequest()
        {
            // Arrange
            var context = new DefaultHttpContext();
            var loggerMock = new Mock<ILogger<ErrorHandlerMiddleware>>();
            var envMock = new Mock<IHostEnvironment>();
            envMock.Setup(e => e.EnvironmentName).Returns(Environments.Production);

            var middleware = new ErrorHandlerMiddleware(
                next: (innerContext) => Task.CompletedTask,
                logger: loggerMock.Object,
                env: envMock.Object);

            // Act
            await middleware.Invoke(context);

            // Assert
            context.Response.Headers.ContainsKey("X-Correlation-ID").Should().BeTrue();
            context.Response.Headers["X-Correlation-ID"].ToString().Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Middleware_InProduction_SanitizesInternalServerErrorMessages()
        {
            // Arrange
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            var loggerMock = new Mock<ILogger<ErrorHandlerMiddleware>>();
            var envMock = new Mock<IHostEnvironment>();
            envMock.Setup(e => e.EnvironmentName).Returns(Environments.Production);

            var middleware = new ErrorHandlerMiddleware(
                next: (innerContext) => throw new InvalidOperationException("Sensitive SQL connection details leaked!"),
                logger: loggerMock.Object,
                env: envMock.Object);

            // Act
            await middleware.Invoke(context);

            // Assert
            context.Response.StatusCode.Should().Be(500);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(context.Response.Body);
            var responseJson = await reader.ReadToEndAsync();

            var doc = JsonDocument.Parse(responseJson);
            var message = doc.RootElement.GetProperty("message").GetString();

            message.Should().NotContain("Sensitive SQL connection details leaked!");
            message.Should().Contain("An unexpected error occurred. Please contact system support.");
        }

        [Fact]
        public async Task Middleware_CloudStorageException_ReturnsBadGatewayWithDescriptiveMessage()
        {
            // Arrange
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            var loggerMock = new Mock<ILogger<ErrorHandlerMiddleware>>();
            var envMock = new Mock<IHostEnvironment>();
            envMock.Setup(e => e.EnvironmentName).Returns(Environments.Production);

            var middleware = new ErrorHandlerMiddleware(
                next: (innerContext) => throw new Acacia.Core.Exeptions.CloudStorageException("Backblaze B2 cloud storage error: Malformed Access Key Id."),
                logger: loggerMock.Object,
                env: envMock.Object);

            // Act
            await middleware.Invoke(context);

            // Assert
            context.Response.StatusCode.Should().Be(502);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(context.Response.Body);
            var responseJson = await reader.ReadToEndAsync();
            var doc = JsonDocument.Parse(responseJson);
            var message = doc.RootElement.GetProperty("message").GetString();

            message.Should().Contain("Backblaze B2 cloud storage error");
        }
    }
}
