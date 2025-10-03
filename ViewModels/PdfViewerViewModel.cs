using _301273104_rosario_lab2.Models;
using System.ComponentModel;
using System.IO;

namespace _301273104_rosario_lab2.ViewModels
{
    public class PdfViewerViewModel : ViewModelBase
    {
        private readonly SelectedBook _selectedBook;

        public PdfViewerViewModel(SelectedBook selectedBook)
        {
            _selectedBook = selectedBook;
            _selectedBook.PropertyChanged += SelectedBookOnPropertyChanged;
        }

        public Book? CurrentBook => _selectedBook.Book;

        /// <summary>
        /// Exposes the in-memory PDF stream for binding to the viewer.
        /// </summary>
        public MemoryStream? DocumentStream => _selectedBook.DocumentStream;

        public int BookmarkPage
        {
            get => _selectedBook.Book?.BookmarkPage ?? 0;
            set
            {
                if (_selectedBook.Book != null && _selectedBook.Book.BookmarkPage != value)
                {
                    _selectedBook.Book.BookmarkPage = value;
                    OnPropertyChanged();
                }
            }
        }

        private void SelectedBookOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            // Relay changes from SelectedBook into this ViewModel
            switch (e.PropertyName)
            {
                case nameof(SelectedBook.Book):
                    OnPropertyChanged(nameof(CurrentBook));
                    OnPropertyChanged(nameof(BookmarkPage));
                    break;

                case nameof(SelectedBook.DocumentStream):
                    OnPropertyChanged(nameof(DocumentStream));
                    break;
            }
        }

    }
}
