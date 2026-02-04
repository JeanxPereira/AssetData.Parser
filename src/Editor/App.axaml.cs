using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AssetData.Parser.Editor.Services;
using AssetData.Parser.Editor.ViewModels;

namespace AssetData.Parser.Editor;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // Initialize Dependency Injection
        var services = ServiceConfiguration.ConfigureServices();
        ServiceLocator.Initialize(services);
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Get ViewModel from DI container
            var viewModel = ServiceLocator.Get<MainViewModel>();
            
            desktop.MainWindow = new MainWindow
            {
                DataContext = viewModel
            };
        }
        
        base.OnFrameworkInitializationCompleted();
    }
}
