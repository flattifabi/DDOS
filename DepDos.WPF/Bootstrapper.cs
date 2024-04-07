using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Authentication.ExtendedProtection;
using DepDos.BusinessLogic.Dos;
using DepDos.Core.Constants;
using DepDos.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
namespace DepDos.WPF;

public class Bootstrapper
{
    private MainWindow? _view;
    private MainWindowViewModel? _viewModel;
    private readonly IServiceProvider _serviceProvider;
    private HttpClient? _client;
    public Bootstrapper()
    {
        var services = new ServiceCollection();
        ConfigureFiles();
        ConfigureServices(services);
        _serviceProvider = services.BuildServiceProvider();
        _client = new HttpClient();
        Show();
    }
    private void Show()
    {
        _view = _serviceProvider.GetService<MainWindow>();
        _viewModel = _serviceProvider.GetService<MainWindowViewModel>();
        _client = _serviceProvider.GetService<HttpClient>();
        
        if (_view == null || _viewModel == null)
            throw new NullReferenceException("MainWindow or MainWindowViewModel was null");
        _view.DataContext = _viewModel;
        _view.Show();
    }
    private void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IDosService, DosService>();
        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<MainWindow>();
        services.AddSingleton<HttpClient>();
    }

    private void ConfigureFiles() => FilePaths.CreateBaseFilePath();
}