using System.Windows.Input;

namespace _301273104_rosario_lab2.Commands
{
    public abstract class CommandBase : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public virtual bool CanExecute(object? parameter) => true;

        public abstract void Execute(object? parameter);

        protected void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
