using Amazon.S3.Model;

namespace _301273104_rosario_lab2.Services
{
    public interface IStorageService
    {
        Task<byte[]> GetObjectAsync(string bucketName, string objectName);
    }
}
