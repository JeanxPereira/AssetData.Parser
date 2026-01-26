using Microsoft.Extensions.DependencyInjection;
using ReCap.Parser;
using ReCap.Parser.Editor.ViewModels;

namespace ReCap.Parser.Editor.Services;

/// <summary>
/// Dependency Injection configuration for the Editor.
/// </summary>
public static class ServiceConfiguration
{
    /// <summary>
    /// Configure all services for the application.
    /// </summary>
    public static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        
        // Core services (singleton - parser is stateless and reusable)
        services.AddSingleton<AssetService>();
        
        // Editor services
        services.AddSingleton<UndoRedoService>();
        // SettingsService is static, no need to register
        
        // ViewModels (transient - each window gets its own)
        services.AddTransient<MainViewModel>();
        
        return services.BuildServiceProvider();
    }
}

/// <summary>
/// Service locator for accessing DI services.
/// Used sparingly - prefer constructor injection where possible.
/// </summary>
public static class ServiceLocator
{
    private static IServiceProvider? _provider;
    
    public static void Initialize(IServiceProvider provider)
    {
        _provider = provider;
    }
    
    public static T Get<T>() where T : notnull
    {
        if (_provider == null)
            throw new InvalidOperationException("ServiceLocator not initialized. Call Initialize() first.");
        
        return _provider.GetRequiredService<T>();
    }
    
    public static T? TryGet<T>() where T : class
    {
        return _provider?.GetService<T>();
    }
}
