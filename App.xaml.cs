using _301273104_rosario_lab2.Factories;
using _301273104_rosario_lab2.Models;
using _301273104_rosario_lab2.Services;
using _301273104_rosario_lab2.Services.Impl;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.S3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Windows;

namespace _301273104_rosario_lab2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;

        public App()
        {
            var services = new ServiceCollection();

            // Build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            services.AddSingleton<IConfiguration>(configuration);

            // Register IAmazonS3 with credentials from configuration
            services.AddSingleton<IAmazonS3>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();

                var region = config["AWS:Region"];
                var accessKey = config["AWS:AccessKey"];
                var secretKey = config["AWS:SecretKey"];

                return new AmazonS3Client(
                    accessKey,
                    secretKey,
                    RegionEndpoint.GetBySystemName(region)
                );
            });

            // Register IAmazonDynamoDB with credentials from configuration
            services.AddSingleton<IAmazonDynamoDB>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();

                var region = config["AWS:Region"];
                var accessKey = config["AWS:AccessKey"];
                var secretKey = config["AWS:SecretKey"];

                return new AmazonDynamoDBClient(
                    accessKey,
                    secretKey,
                    RegionEndpoint.GetBySystemName(region)
                );
            });

            // Register Models
            services.AddSingleton<User>();
            services.AddSingleton<Bookshelf>();
            services.AddSingleton<SelectedBook>();

            // Register Factories
            services.AddSingleton<IWindowFactory, WindowFactory>();

            // Register storage service
            services.AddSingleton<IStorageService, S3StorageService>();
            services.AddSingleton<IRepository, DynamoDBRepository>();

            // Register Commands
            services.AddTransient<Commands.LoginCommand>();
            services.AddTransient<Commands.LogoutCommand>();
            services.AddTransient<Commands.LoadBooksCommand>();
            services.AddTransient<Commands.OpenPdfViewerCommand>();
            services.AddTransient<Commands.UpdateBookCommand>();

            // Register ViewModels
            services.AddTransient<ViewModels.MainWindowViewModel>();
            services.AddTransient<ViewModels.BookshelfViewModel>();
            services.AddTransient<ViewModels.PdfViewerViewModel>();

            // Register Views
            services.AddTransient<Views.MainWindow>();
            services.AddTransient<Views.BookshelfWindow>();
            services.AddTransient<Views.PdfViewerWindow>();

            _serviceProvider = services.BuildServiceProvider();

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Resolve LoginWindow from DI
            var loginWindow = _serviceProvider.GetRequiredService<Views.MainWindow>();
            loginWindow.DataContext = _serviceProvider.GetRequiredService<ViewModels.MainWindowViewModel>();
            loginWindow.Show();
        }


    }

}
