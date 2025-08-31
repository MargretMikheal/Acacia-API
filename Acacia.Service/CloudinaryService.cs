using Acacia.Core.Interfaces.Services;
using Acacia.Core.Models.Cloudinary;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Acacia.Service;

public class CloudinaryService : ICloudinaryService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryService(Cloudinary cloudinary)
    {
        _cloudinary = cloudinary;
    }

    public async Task<CloudinaryImageResult> UploadImageAsync(IFormFile file, string folder, CancellationToken token)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("File is empty");

        await using var stream = file.OpenReadStream();
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            Folder = folder
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams, token);

        if (uploadResult.Error != null)
            throw new Exception(uploadResult.Error.Message);

        return new CloudinaryImageResult
        {
            PublicId = uploadResult.PublicId,
            Url = uploadResult.SecureUrl.AbsoluteUri
        };
    }

    public async Task<bool> DeleteImageAsync(string publicId, CancellationToken token)
    {
        if (string.IsNullOrEmpty(publicId))
            throw new ArgumentException("PublicId cannot be null or empty");

        var deletionParams = new DeletionParams(publicId)
        {
            ResourceType = ResourceType.Image
        };

        var deletionResult = await _cloudinary.DestroyAsync(deletionParams);

        return deletionResult.Result == "ok";
    }
}
