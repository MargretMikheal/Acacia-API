using Acacia.Core.Models.Cloudinary;
using Microsoft.AspNetCore.Http;

namespace Acacia.Core.Interfaces.Services;

public interface ICloudinaryService
{
    Task<CloudinaryImageResult> UploadImageAsync(IFormFile file, string folder, CancellationToken token);
    Task<bool> DeleteImageAsync(string publicId, CancellationToken token);
}
