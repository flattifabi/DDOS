using System.Collections.ObjectModel;
using System.Windows;
using DepDos.Core.Helpers;
using DepDos.Core.Interfaces;
using DepDos.Core.Models;
using DepDos.WPF.Objects;

namespace DepDos.WPF;

public class MainWindowViewModel : BaseViewModel
{
    public DosConfiguration DosConfiguration { get; set; } = new DosConfiguration();
    public IDelegateCommand CloseCommand => new DelegateCommand(() => Application.Current.Shutdown());
    public IDelegateCommand MinimizeCommand => new DelegateCommand(() =>
    {
        _mainWindow.WindowState = WindowState.Minimized;
    });

    private readonly MainWindow _mainWindow;
    private readonly IDosService _dosService;

    public MainWindowViewModel(MainWindow mainWindow, IDosService dosService)
    {
        _mainWindow = mainWindow;
        _dosService = dosService;
        Initialize();
    }
    private void Initialize()
    {
        
    }
}