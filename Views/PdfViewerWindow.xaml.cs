using _301273104_rosario_lab2.ViewModels;
using System.Windows;

namespace _301273104_rosario_lab2.Views
{
    /// <summary>
    /// Interaction logic for PdfViewerWindow.xaml
    /// </summary>
    public partial class PdfViewerWindow : Window
    {
        public PdfViewerWindow(PdfViewerViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
