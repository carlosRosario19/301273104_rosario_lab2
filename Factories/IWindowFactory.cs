using System.Windows;

namespace _301273104_rosario_lab2.Factories
{
    public interface IWindowFactory
    {
        T Create<T>() where T : Window;
    }
}
