using _301273104_rosario_lab2.Models;
using _301273104_rosario_lab2.Services;
using Amazon.DynamoDBv2.Model;
using System.Windows;

namespace _301273104_rosario_lab2.Commands
{
    public class LoadBooksCommand : CommandBase
    {
        private readonly User _user;
        private readonly Bookshelf _bookshelf;
        private readonly IRepository _dynamoDB;

        public LoadBooksCommand(User user, Bookshelf bookshelf, IRepository dynamoDB)
        {
            _user = user;
            _bookshelf = bookshelf;
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
                var username = _user.Username;
                if (string.IsNullOrWhiteSpace(username))
                {
                    MessageBox.Show("No user is logged in. Please login first.", "Load Books", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Fetch raw items from repository (ordered by lastAccessed via your GSI)
                var items = await _dynamoDB.GetUserBooksOrderedAsync(username);

                // Clear existing books on UI thread
                Application.Current.Dispatcher.Invoke(() => _bookshelf.Books.Clear());

                foreach (var item in items)
                {
                    // Title
                    var title = item.ContainsKey("title") && item["title"].S != null
                        ? item["title"].S
                        : string.Empty;

                    // Authors (list of AttributeValue.L)
                    var authors = new List<string>();
                    if (item.ContainsKey("authors") && item["authors"].L != null)
                    {
                        foreach (var a in item["authors"].L)
                        {
                            if (a.S != null)
                                authors.Add(a.S);
                        }
                    }

                    // s3Key
                    var s3Key = item.ContainsKey("s3Key") && item["s3Key"].S != null
                        ? item["s3Key"].S
                        : string.Empty;

                    var book = new Book
                    {
                        Title = title,
                        Authors = authors,
                        S3Key = s3Key
                    };

                    // Add to ObservableCollection on UI thread
                    Application.Current.Dispatcher.Invoke(() => _bookshelf.Books.Add(book));
                }
            }
            catch (ResourceNotFoundException ex)
            {
                MessageBox.Show($"DynamoDB table not found: {ex.Message}", "AWS Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading books: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
