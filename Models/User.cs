using System.ComponentModel;

namespace _301273104_rosario_lab2.Models
{
    public class User : INotifyPropertyChanged
    {
        private string? _username;
        
        public string? Username
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    _username = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Username)));
                }
            }
        }

        private string? _password;

        public string? Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Password)));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void Clear()
        {
            Username = string.Empty;
            Password = string.Empty;
        }
    }
}
