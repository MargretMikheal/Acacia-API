using Amazon.S3;
using Amazon.S3.Model;
using Acacia.Core.Exeptions;
using Acacia.Core.Interfaces.Services;
using Acacia.Core.Models.Cloudinary;
using Acacia.Core.Models.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Acacia.Service;

public class BackblazeStorageService : IStorageService, ICloudinaryService
{
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName;
    private readonly string _friendlyUrlBase;
    private readonly ILogger<BackblazeStorageService> _logger;

    public BackblazeStorageService(
        IOptions<BackblazeSettings> settings,
        IAmazonS3 s3Client,
        ILogger<BackblazeStorageService> logger)
    {
        _s3Client = s3Client;
        _logger = logger;

        var config = settings.Value;
        _bucketName = string.IsNullOrWhiteSpace(config.BucketName) ? "acacia-storage" : config.BucketName;
        var region = string.IsNullOrWhiteSpace(config.Region) ? "us-east-005" : config.Region;
        _friendlyUrlBase = $"https://{_bucketName}.s3.{region}.backblazeb2.com";
    }

    public async Task<StorageUploadResult> UploadAsync(IFormFile file, string folder, CancellationToken cancellationToken = default)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("File cannot be null or empty.", nameof(file));

        var sanitizedFileName = Path.GetFileName(file.FileName).Replace(" ", "_");
        var key = $"{folder.TrimEnd('/')}/{Guid.NewGuid()}_{sanitizedFileName}";

        try
        {
            await using var stream = file.OpenReadStream();
            var uploadRequest = new PutObjectRequest
            {
                InputStream = stream,
                Key = key,
                BucketName = _bucketName,
                ContentType = file.ContentType ?? "application/octet-stream",
                DisablePayloadSigning = true
            };

            await _s3Client.PutObjectAsync(uploadRequest, cancellationToken);

            var presignedUrl = GetPresignedUrl(key, 10080);
            return new StorageUploadResult
            {
                FileId = key,
                Url = presignedUrl
            };
        }
        catch (AmazonS3Exception s3Ex)
        {
            _logger.LogError(s3Ex, "Backblaze B2 S3 storage failure for key {Key}: {Message}", key, s3Ex.Message);
            throw new CloudStorageException($"Backblaze B2 cloud storage error: {s3Ex.Message}. Please verify your S3 Application Key ID and Secret in Backblaze settings.", s3Ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to upload file {FileName} to Backblaze B2 key {Key}", file.FileName, key);
            throw new CloudStorageException($"Failed to upload file to cloud storage: {ex.Message}", ex);
        }
    }

    public async Task<bool> DeleteAsync(string fileId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(fileId))
            return true;

        try
        {
            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = _bucketName,
                Key = fileId
            };

            await _s3Client.DeleteObjectAsync(deleteRequest, cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete file with ID {FileId} from Backblaze B2", fileId);
            return false;
        }
    }

    public string GetPresignedUrl(string fileId, double durationInMinutes = 10080)
    {
        if (string.IsNullOrWhiteSpace(fileId))
            return string.Empty;

        var request = new GetPreSignedUrlRequest
        {
            BucketName = _bucketName,
            Key = fileId,
            Verb = HttpVerb.GET,
            Expires = DateTime.UtcNow.AddMinutes(durationInMinutes)
        };

        return _s3Client.GetPreSignedURL(request);
    }

    public string? ResolvePresignedUrl(string? fileIdOrUrl, double durationInMinutes = 10080)
    {
        if (string.IsNullOrWhiteSpace(fileIdOrUrl))
            return null;

        try
        {
            if (Uri.TryCreate(fileIdOrUrl, UriKind.Absolute, out var uri))
            {
                if (!uri.Host.Contains("backblazeb2.com", StringComparison.OrdinalIgnoreCase))
                    return fileIdOrUrl;

                var path = uri.AbsolutePath.TrimStart('/');
                if (path.StartsWith("file/", StringComparison.OrdinalIgnoreCase))
                {
                    path = path.Substring(5);
                }

                if (path.StartsWith($"{_bucketName}/", StringComparison.OrdinalIgnoreCase))
                {
                    path = path.Substring(_bucketName.Length + 1);
                }

                var key = Uri.UnescapeDataString(path);
                return GetPresignedUrl(key, durationInMinutes);
            }

            return GetPresignedUrl(fileIdOrUrl, durationInMinutes);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to resolve presigned URL for file/URL: {Input}", fileIdOrUrl);
            return fileIdOrUrl;
        }
    }

    #region ICloudinaryService Adapter for Backward Compatibility
    public async Task<CloudinaryImageResult> UploadImageAsync(IFormFile file, string folder, CancellationToken token)
    {
        var result = await UploadAsync(file, folder, token);
        return new CloudinaryImageResult
        {
            PublicId = result.FileId,
            Url = result.Url
        };
    }

    public async Task<bool> DeleteImageAsync(string publicId, CancellationToken token)
    {
        return await DeleteAsync(publicId, token);
    }
    #endregion
}
