using _301273104_rosario_lab2.Factories;
using Amazon;
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

            // Register Factories
            services.AddSingleton<IWindowFactory, WindowFactory>();

            // Register ViewModels
            services.AddTransient<ViewModels.MainWindowViewModel>();

            // Register Views
            services.AddTransient<Views.MainWindow>();

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
