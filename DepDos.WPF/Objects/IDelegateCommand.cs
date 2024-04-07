using System.Windows.Input;

namespace DepDos.WPF.Objects;

public interface IDelegateCommand : ICommand
{ }
public class DelegateCommand : IDelegateCommand
{
    private readonly Action _execute;
    private readonly Func<bool> _canExecute;

    public DelegateCommand(Action execute) : this(execute, null)
    {
    }

    public DelegateCommand(Action execute, Func<bool> canExecute)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object parameter)
    {
        return _canExecute?.Invoke() ?? true;
    }

    public void Execute(object parameter)
    {
        _execute();
    }
}

public class DelegateCommand<T> : IDelegateCommand
{
    private readonly Action<T> _execute;

    public DelegateCommand(Action<T> execute)
    {
        _execute = execute;
    }

    public bool CanExecute(object parameter)
    {
        return true;
    }

    public void Execute(object parameter)
    {
        _execute((T)parameter);
    }

#pragma warning disable CS0067 // Das Ereignis "TypeRelayCommand<T>.CanExecuteChanged" wird nie verwendet.
    public event EventHandler CanExecuteChanged;
#pragma warning restore CS0067 // Das Ereignis "TypeRelayCommand<T>.CanExecuteChanged" wird nie verwendet.
}