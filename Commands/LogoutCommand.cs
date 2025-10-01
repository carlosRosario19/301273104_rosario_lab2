using _301273104_rosario_lab2.Factories;
using _301273104_rosario_lab2.Models;
using _301273104_rosario_lab2.Views;
using System.Windows;

namespace _301273104_rosario_lab2.Commands
{
    public class LogoutCommand : CommandBase
    {
        private readonly User _user;
        private readonly IWindowFactory _windowFactory;

        public LogoutCommand(User user, IWindowFactory windowFactory)
        {
            _user = user;
            _windowFactory = windowFactory;
        }
        public override void Execute(object? parameter)
        {
            _ = ExecuteAsync();
        }

        private async Task ExecuteAsync()
        {
            _user.Username = string.Empty;
            _user.Password = string.Empty;

            // Open Login Window
            var window = _windowFactory.Create<MainWindow>();
            window.Show();

            // Close MainWindow
            Application.Current.Windows
                .OfType<BookshelfWindow>()
                .FirstOrDefault()?
                .Close();
        }
    }
}
