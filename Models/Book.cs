namespace _301273104_rosario_lab2.Models
{
    public class Book
    {
        public string Title { get; set; } = string.Empty;
        public List<string> Authors { get; set; } = new();
        public string S3Key { get; set; } = string.Empty;
        public int BookmarkPage { get; set; }
        public DateTime LastAccessed { get; set; }
    }
}
