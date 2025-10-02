using System.Collections.ObjectModel;

namespace _301273104_rosario_lab2.Models
{
    public class Bookshelf
    {
        public ObservableCollection<Book> Books { get; set; } = new();
    }
}
