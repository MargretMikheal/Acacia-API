namespace Acacia.Core.Models.Storage;

public class BackblazeSettings
{
    public string BucketName { get; set; } = string.Empty;
    public string Region { get; set; } = "us-east-005";
    public string Endpoint { get; set; } = string.Empty;
    public string AccessKey { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
}
