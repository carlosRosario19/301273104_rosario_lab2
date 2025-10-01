using _301273104_rosario_lab2.Commands;
using _301273104_rosario_lab2.Models;

namespace _301273104_rosario_lab2.ViewModels
{
    public class BookshelfViewModel : ViewModelBase
    {
        private readonly User _user;
        
        public CommandBase LogoutCommand { get; }

        public BookshelfViewModel(User user, LogoutCommand logoutCommand)
        {
            _user = user;
            LogoutCommand = logoutCommand;
        }

        public string? Username
        {
            get => _user.Username;
            set => _user.Username = value;
        }
    }
}
