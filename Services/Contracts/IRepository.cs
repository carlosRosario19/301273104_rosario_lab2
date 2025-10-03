using Amazon.DynamoDBv2.Model;

namespace _301273104_rosario_lab2.Services
{
    public interface IRepository
    {
        Task<bool> ValidateCredentialsAsync(string username, string password);
        Task<List<Dictionary<string, AttributeValue>>> GetUserBooksOrderedAsync(string username);
        Task UpdateBookProgressAsync(string username, string isbn, int bookmarkPage, DateTime lastAccessed);
    }
}
