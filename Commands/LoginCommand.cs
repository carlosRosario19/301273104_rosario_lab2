using _301273104_rosario_lab2.Factories;
using _301273104_rosario_lab2.Models;
using _301273104_rosario_lab2.Services;
using _301273104_rosario_lab2.Views;
using System.Windows;

namespace _301273104_rosario_lab2.Commands
{
    public class LoginCommand : CommandBase
    {
        private readonly User _user;
        private readonly IWindowFactory _windowFactory;
        private readonly IRepository _dynamoDB;

        public LoginCommand(User user, IWindowFactory windowFactory, IRepository dynamoDB)
        {
            _user = user;
            _windowFactory = windowFactory;
            _dynamoDB = dynamoDB;
        }

        public override void Execute(object? parameter)
        {
            _ = ExecuteAsync();
        }

        private async Task ExecuteAsync()
        {
            try
            {
                // Validate credentials
                bool isValid = await _dynamoDB.ValidateCredentialsAsync(_user.Username ?? "", _user.Password ?? "");

                if (isValid)
                {
                    // Open BookshelfWindow
                    var window = _windowFactory.Create<BookshelfWindow>();
                    window.Show();

                    // Close MainWindow
                    Application.Current.Windows
                        .OfType<MainWindow>()
                        .FirstOrDefault()?
                        .Close();
                }
                else
                {
                    // Invalid credentials
                    MessageBox.Show(
                        "Incorrect username or password. Please try again.",
                        "Login Failed",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning
                    );
                }
            }
            catch (Amazon.DynamoDBv2.Model.ResourceNotFoundException ex)
            {
                // Table does not exist
                MessageBox.Show(
                    $"DynamoDB table not found: {ex.Message}",
                    "AWS Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
            catch (Exception ex)
            {
                // Other errors
                MessageBox.Show(
                    $"An error occurred: {ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }
    }
}
