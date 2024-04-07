using System.Configuration;
using System.Data;
using System.Windows;

namespace DepDos.WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private Bootstrapper _bootstrapper;
    private void App_OnStartup(object sender, StartupEventArgs e)
    {
        _bootstrapper = new Bootstrapper();
    }
}