
using Amazon.S3;
using Amazon.S3.Model;
using System.IO;

namespace _301273104_rosario_lab2.Services
{
    public class S3StorageService : IStorageService
    {
        public IAmazonS3 Client;

        public S3StorageService(IAmazonS3 client)
        {
            Client = client;
        }

        public async Task<byte[]> GetObjectAsync(string bucketName, string objectName)
        {
            try
            {
                var request = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = objectName
                };

                using (var response = await Client.GetObjectAsync(request))
                using (var memoryStream = new MemoryStream())
                {
                    await response.ResponseStream.CopyToAsync(memoryStream);
                    return memoryStream.ToArray(); // PDF bytes in memory
                }
            }
            catch (AmazonS3Exception ex)
            {
                Console.WriteLine($"Error getting object from S3: {ex.Message}");
                throw;
            }
        }
    }
}
