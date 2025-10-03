using _301273104_rosario_lab2.Models;
using _301273104_rosario_lab2.ViewModels;
using System.Windows;
using System.Windows.Input;

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

            // Run LoadBooksCommand when the window finishes loading
            Loaded += (_, __) =>
            {
                if (viewModel.LoadBooksCommand.CanExecute(null))
                    viewModel.LoadBooksCommand.Execute(null);
            };
        }

        private void BooksListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var vm = DataContext as BookshelfViewModel;
            if (vm == null) return;

            var book = BooksListBox.SelectedItem as Book;
            if (book == null) return;

            if (vm.OpenPdfViewerCommand != null && vm.OpenPdfViewerCommand.CanExecute(book))
                vm.OpenPdfViewerCommand.Execute(book);
        }
    }
}
