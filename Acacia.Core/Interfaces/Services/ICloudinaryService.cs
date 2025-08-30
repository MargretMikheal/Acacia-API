using Microsoft.AspNetCore.Http;

namespace Acacia.Core.Interfaces.Services;

public interface ICloudinaryService
{
    Task<string> UploadImageAsync(IFormFile file, string folder, CancellationToken token);
}
