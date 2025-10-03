using System.IO;

namespace _301273104_rosario_lab2.Services
{
    public interface IStorageService
    {
        Task<MemoryStream> GetObjectAsync(string bucketName, string objectName);
    }
}
