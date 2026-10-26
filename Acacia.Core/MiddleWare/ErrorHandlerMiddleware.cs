using Acacia.Core.Bases;
using Acacia.Core.Exeptions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace Acacia.Core.MiddleWare
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ErrorHandlerMiddleware(
            RequestDelegate next,
            ILogger<ErrorHandlerMiddleware> logger,
            IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            var correlationId = context.Request.Headers["X-Correlation-ID"].FirstOrDefault()
                ?? context.TraceIdentifier;
            context.Response.Headers["X-Correlation-ID"] = correlationId;

            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                var responseModel = new Response<string>
                {
                    Succeeded = false,
                    Message = error.Message,
                    Data = null,
                    Errors = null,
                    Response_Code = HttpStatusCode.InternalServerError
                };

                switch (error)
                {
                    case UnauthorizedAccessException e:
                        _logger.LogWarning("Unauthorized access attempt at {Path} (CorrelationId: {CorrelationId}): {Message}",
                            context.Request.Path, correlationId, e.Message);
                        responseModel.Message = e.Message;
                        responseModel.Response_Code = HttpStatusCode.Unauthorized;
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;

                    case ValidationExceptionWithErrors e:
                        _logger.LogWarning("Validation failure at {Path} (CorrelationId: {CorrelationId}) with {Count} errors",
                            context.Request.Path, correlationId, e.Errors?.Count ?? 0);
                        responseModel.Message = e.Message;
                        responseModel.Errors = e.Errors;
                        responseModel.Response_Code = HttpStatusCode.UnprocessableEntity;
                        response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                        break;

                    case ValidationException e:
                        _logger.LogWarning("Validation exception at {Path} (CorrelationId: {CorrelationId}): {Message}",
                            context.Request.Path, correlationId, e.Message);
                        responseModel.Message = e.Message;
                        responseModel.Response_Code = HttpStatusCode.UnprocessableEntity;
                        response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                        break;

                    case KeyNotFoundException e:
                        _logger.LogInformation("Resource not found at {Path} (CorrelationId: {CorrelationId}): {Message}",
                            context.Request.Path, correlationId, e.Message);
                        responseModel.Message = e.Message;
                        responseModel.Response_Code = HttpStatusCode.NotFound;
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;

                    case DbUpdateException e:
                        _logger.LogError(e, "Database update exception occurred at {Path} (CorrelationId: {CorrelationId})",
                            context.Request.Path, correlationId);
                        responseModel.Message = _env.IsDevelopment()
                            ? e.InnerException?.Message ?? e.Message
                            : "A database error occurred while processing your request.";
                        responseModel.Response_Code = HttpStatusCode.BadRequest;
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    case CloudStorageException e:
                        _logger.LogError(e, "Cloud storage exception occurred at {Path} (CorrelationId: {CorrelationId})",
                            context.Request.Path, correlationId);
                        responseModel.Message = e.Message;
                        responseModel.Response_Code = HttpStatusCode.BadGateway;
                        response.StatusCode = (int)HttpStatusCode.BadGateway;
                        break;

                    default:
                        _logger.LogError(error, "Unhandled exception processing request {Path} (CorrelationId: {CorrelationId})",
                            context.Request.Path, correlationId);
                        responseModel.Message = _env.IsDevelopment()
                            ? (error.InnerException != null ? $"{error.Message}\n{error.InnerException.Message}" : error.Message)
                            : "An unexpected error occurred. Please contact system support.";
                        responseModel.Response_Code = HttpStatusCode.InternalServerError;
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var jsonOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                };
                var result = JsonSerializer.Serialize(responseModel, jsonOptions);
                await response.WriteAsync(result);
            }
        }
    }
}
