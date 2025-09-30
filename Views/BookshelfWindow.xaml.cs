using _301273104_rosario_lab2.ViewModels;
using System.Windows;

namespace _301273104_rosario_lab2.Views
{
    /// <summary>
    /// Interaction logic for BookshelfWindow.xaml
    /// </summary>
    public partial class BookshelfWindow : Window
    {
        public BookshelfWindow(BookshelfViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
