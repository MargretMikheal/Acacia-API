using Acacia.Core.Models.Storage;
using Microsoft.AspNetCore.Http;

namespace Acacia.Core.Interfaces.Services;

public interface IStorageService
{
    Task<StorageUploadResult> UploadAsync(IFormFile file, string folder, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(string fileId, CancellationToken cancellationToken = default);
    string GetPresignedUrl(string fileId, double durationInMinutes = 10080);
    string? ResolvePresignedUrl(string? fileIdOrUrl, double durationInMinutes = 10080);
}
