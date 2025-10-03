using System.ComponentModel;
using System.IO;

namespace _301273104_rosario_lab2.Models
{
    public class SelectedBook : INotifyPropertyChanged
    {
        private Book? _book;
        private MemoryStream? _documentStream;

        public Book? Book
        {
            get => _book;
            set
            {
                if (_book != value)
                {
                    _book = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Book)));
                }
            }
        }

        /// <summary>
        /// Holds the in-memory PDF file bytes for the selected book.
        /// This gets set when the PDF is fetched from S3.
        /// </summary>
        public MemoryStream? DocumentStream
        {
            get => _documentStream;
            set
            {
                if (_documentStream != value)
                {
                    _documentStream = value;
                    OnPropertyChanged(nameof(MemoryStream));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public void Clear()
        {
            // Dispose the memory stream if it's holding data
            if (_documentStream != null)
            {
                _documentStream.Dispose();
                _documentStream = null;
            }

            Book = null; // this will raise PropertyChanged for Book
            OnPropertyChanged(nameof(DocumentStream));
        }
    }
}
