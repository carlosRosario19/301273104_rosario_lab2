using _301273104_rosario_lab2.Factories;
using _301273104_rosario_lab2.Models;
using _301273104_rosario_lab2.Services;
using _301273104_rosario_lab2.Views;
using System.Windows;

namespace _301273104_rosario_lab2.Commands
{
    public class OpenPdfViewerCommand : CommandBase
    {
        private readonly SelectedBook _selectedBook;
        private readonly IStorageService _storageService;
        private readonly IWindowFactory _windowFactory;
        private const string _bucketName = "my-aws-books-bucket";

        public OpenPdfViewerCommand(SelectedBook selectedBook, IStorageService storageService, IWindowFactory windowFactory)
        {
            _selectedBook = selectedBook;
            _storageService = storageService;
            _windowFactory = windowFactory;
        }

        public override void Execute(object? parameter)
        {
            _ = ExecuteAsync();
        }

        private async Task ExecuteAsync()
        {
            if (_selectedBook.Book == null)
                return;

            try
            {
                // 1. Pull PDF from S3
                var memoryStream = await _storageService.GetObjectAsync(_bucketName, _selectedBook.Book.S3Key);

                // 2. Store PDF bytes in SelectedBook (acts as shared state)
                _selectedBook.Book.LastAccessed = DateTime.UtcNow;
                _selectedBook.DocumentStream = memoryStream;

                // 3. Open PdfViewerWindow
                var window = _windowFactory.Create<PdfViewerWindow>();
                window.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open PDF: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
