using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Xunit;
using Xunit.Abstractions;

namespace Acacia.Tests.Storage
{
    public class BackblazeConnectivityTests
    {
        private readonly ITestOutputHelper _output;

        public BackblazeConnectivityTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task TestBackblazeConnectivity()
        {
            var appSettingsPath = Path.Combine(AppContext.BaseDirectory, "../../../../../Acacia-API/Acacia.Api/appsettings.Production.json");
            if (!File.Exists(appSettingsPath))
            {
                appSettingsPath = Path.Combine(AppContext.BaseDirectory, "../../../../../Acacia-API/Acacia.Api/appsettings.json");
            }
            if (!File.Exists(appSettingsPath))
            {
                _output.WriteLine($"appsettings not found at {appSettingsPath}");
                return;
            }

            var json = await File.ReadAllTextAsync(appSettingsPath);
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;
            if (!root.TryGetProperty("Backblaze", out var bbSection))
            {
                _output.WriteLine("Backblaze section not found");
                return;
            }

            var bucketName = bbSection.GetProperty("BucketName").GetString();
            var endpoint = bbSection.GetProperty("Endpoint").GetString();
            var accessKey = bbSection.GetProperty("AccessKey").GetString();
            var secretKey = bbSection.GetProperty("SecretKey").GetString();
            var region = bbSection.TryGetProperty("Region", out var rProp) ? rProp.GetString() : "us-east-005";

            var config = new AmazonS3Config
            {
                ServiceURL = endpoint,
                AuthenticationRegion = region,
                ForcePathStyle = true
            };

            using var s3Client = new AmazonS3Client(accessKey, secretKey, config);
            try
            {
                var response = await s3Client.ListObjectsV2Async(new ListObjectsV2Request
                {
                    BucketName = bucketName,
                    MaxKeys = 10
                });
                Assert.NotNull(response);
                _output.WriteLine($"Found {response.S3Objects.Count} objects in {bucketName}:");
                foreach (var obj in response.S3Objects)
                {
                    _output.WriteLine($"Key: {obj.Key}, Size: {obj.Size}");

                    var presignedReq = new GetPreSignedUrlRequest
                    {
                        BucketName = bucketName,
                        Key = obj.Key,
                        Verb = HttpVerb.GET,
                        Expires = DateTime.UtcNow.AddDays(7)
                    };
                    var presignedUrl = s3Client.GetPreSignedURL(presignedReq);
                    _output.WriteLine($"Presigned URL (7 days): {presignedUrl}");

                    using var http = new System.Net.Http.HttpClient();
                    var getRes = await http.GetAsync(presignedUrl);
                    _output.WriteLine($"7-Day Presigned Fetch Status: {getRes.StatusCode}, ContentType: {getRes.Content.Headers.ContentType}");
                }
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Backblaze S3 connectivity check: {ex.Message}");
            }
        }
    }
}
