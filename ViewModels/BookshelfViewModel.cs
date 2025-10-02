using _301273104_rosario_lab2.Commands;
using _301273104_rosario_lab2.Models;
using System.Collections.ObjectModel;

namespace _301273104_rosario_lab2.ViewModels
{
    public class BookshelfViewModel : ViewModelBase
    {
        private readonly User _user;
        private readonly Bookshelf _bookshelf;

        public CommandBase LogoutCommand { get; }
        public CommandBase LoadBooksCommand { get; }

        public BookshelfViewModel(
            User user, 
            LogoutCommand logoutCommand,
            Bookshelf bookshelf,
            LoadBooksCommand loadBooksCommand)
        {
            _user = user;
            LogoutCommand = logoutCommand;
            _bookshelf = bookshelf;
            LoadBooksCommand = loadBooksCommand;
        }

        public string? Username
        {
            get => _user.Username;
            set => _user.Username = value;
        }

        public ObservableCollection<Book> Books => _bookshelf.Books;
    }
}
