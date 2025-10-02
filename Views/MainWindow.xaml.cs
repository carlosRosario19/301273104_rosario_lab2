using _301273104_rosario_lab2.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace _301273104_rosario_lab2.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainWindowViewModel vm)
            {
                vm.Password = ((PasswordBox)sender).Password;
            }
        }
    }
}