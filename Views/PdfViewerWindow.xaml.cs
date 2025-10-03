using _301273104_rosario_lab2.ViewModels;
using System.Windows;

namespace _301273104_rosario_lab2.Views
{
    /// <summary>
    /// Interaction logic for PdfViewerWindow.xaml
    /// </summary>
    public partial class PdfViewerWindow : Window
    {
        private PdfViewerViewModel? _vm;

        public PdfViewerWindow(PdfViewerViewModel viewModel)
        {
            InitializeComponent();
            _vm = viewModel;
            DataContext = _vm;

            // Subscribe to VM property changes
            _vm.PropertyChanged += VmOnPropertyChanged;
        }

        private void PdfViewer_Loaded(object sender, RoutedEventArgs e)
        {
            LoadPdf();
        }

        private void VmOnPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(PdfViewerViewModel.DocumentStream))
            {
                Dispatcher.Invoke(LoadPdf);
            }
        }

        private void LoadPdf()
        {
            if (_vm?.DocumentStream != null)
            {
                // Reset stream position before reading
                _vm.DocumentStream.Position = 0;

                PdfViewer.Load(_vm.DocumentStream);

                if (_vm.BookmarkPage > 0)
                {
                    PdfViewer.GoToPageAtIndex(_vm.BookmarkPage);
                }
            }
        }
    }
}
