using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace _301273104_rosario_lab2.Factories
{
    public class WindowFactory : IWindowFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public WindowFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public T Create<T>() where T : Window
        {
            return _serviceProvider.GetRequiredService<T>();
        }
    }
}
