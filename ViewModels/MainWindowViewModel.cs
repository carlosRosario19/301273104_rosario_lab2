using _301273104_rosario_lab2.Commands;
using _301273104_rosario_lab2.Models;

namespace _301273104_rosario_lab2.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        private readonly User _user;

        public CommandBase LoginCommand { get; }

        public MainWindowViewModel(User user, LoginCommand loginCommand)
        {
            _user = user;
            LoginCommand = loginCommand;
        }

        public string? Username
        {
            get => _user.Username;
            set => _user.Username = value;
        }

        public string? Password
        {
            get => _user.Password;
            set => _user.Password = value;
        }

    }
}
