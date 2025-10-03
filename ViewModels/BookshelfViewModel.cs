using _301273104_rosario_lab2.Commands;
using _301273104_rosario_lab2.Models;
using System.Collections.ObjectModel;

namespace _301273104_rosario_lab2.ViewModels
{
    public class BookshelfViewModel : ViewModelBase
    {
        private readonly User _user;
        private readonly Bookshelf _bookshelf;
        private readonly SelectedBook _selectedBook;

        public CommandBase LogoutCommand { get; }
        public CommandBase LoadBooksCommand { get; }

        public CommandBase OpenPdfViewerCommand { get; }

        public BookshelfViewModel(
            User user, 
            LogoutCommand logoutCommand,
            Bookshelf bookshelf,
            SelectedBook selectedBook,
            LoadBooksCommand loadBooksCommand,
            OpenPdfViewerCommand openPdfViewerCommand)
        {
            _user = user;
            _bookshelf = bookshelf;
            _selectedBook = selectedBook;
            LogoutCommand = logoutCommand;
            LoadBooksCommand = loadBooksCommand;
            OpenPdfViewerCommand = openPdfViewerCommand;
        }

        public string? Username
        {
            get => _user.Username;
            set => _user.Username = value;
        }

        public Book? SelectedBook
        {
            get => _selectedBook.Book;
            set
            {
                if (_selectedBook.Book != value)
                {
                    _selectedBook.Book = value;
                    OnPropertyChanged(nameof(SelectedBook));
                }
            }
        }

        public ObservableCollection<Book> Books => _bookshelf.Books;
    }
}
