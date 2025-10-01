namespace _301273104_rosario_lab2.Services
{
    public interface IRepository
    {
        Task<bool> ValidateCredentialsAsync(string username, string password);
    }
}
