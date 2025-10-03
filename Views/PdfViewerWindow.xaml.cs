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

            // Listen for page changes in the PDF viewer
            PdfViewer.CurrentPageChanged += PdfViewer_CurrentPageChanged;

            // Listen for window closing
            Closing += PdfViewerWindow_Closing;
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

        private void PdfViewer_CurrentPageChanged(object sender, System.EventArgs e)
        {
            if (_vm != null && _vm.CurrentBook != null)
            {
                // Save the current page back to the ViewModel
                _vm.BookmarkPage = PdfViewer.CurrentPageIndex;
            }
        }

        private void PdfViewerWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            // Persist progress when user closes the window
            _vm?.UpdateBookCommand.Execute(null);
        }
    }
}
