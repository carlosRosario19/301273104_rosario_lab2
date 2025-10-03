using _301273104_rosario_lab2.Models;
using _301273104_rosario_lab2.Services;

namespace _301273104_rosario_lab2.Commands
{
    public class UpdateBookCommand : CommandBase
    {
        private readonly IRepository _dynamoDB;
        private readonly SelectedBook _selectedBook;
        private readonly User _user;

        public UpdateBookCommand(IRepository dynamoDB, SelectedBook selectedBook, User user)
        {
            _dynamoDB = dynamoDB;
            _selectedBook = selectedBook;
            _user = user;
        }

        public override void Execute(object? parameter)
        {
            _ = ExecuteAsync();
        }

        private async Task ExecuteAsync()
        {
            if (_selectedBook.Book == null || string.IsNullOrWhiteSpace(_user.Username))
                return;

            await _dynamoDB.UpdateBookProgressAsync(
                _user.Username,
                _selectedBook.Book.Isbn,
                _selectedBook.Book.BookmarkPage,
                _selectedBook.Book.LastAccessed
            );

            // Clear the selected book
            _selectedBook.Clear();
        }
    }
}
