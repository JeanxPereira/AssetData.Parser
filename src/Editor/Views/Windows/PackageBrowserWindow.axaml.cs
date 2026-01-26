using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using ReCap.Parser;
using ReCap.Parser.Editor.Services;
using ReCap.Parser.Editor.ViewModels;

namespace ReCap.Parser.Editor.Views.Windows;

public partial class PackageBrowserWindow : Window
{
    private PackageBrowserViewModel? ViewModel => DataContext as PackageBrowserViewModel;
    
    public PackageBrowserWindow()
    {
        InitializeComponent();
    }
    
    public PackageBrowserWindow(AssetService assetService) : this()
    {
        DataContext = new PackageBrowserViewModel(assetService);
    }
    
    /// <summary>
    /// Event raised when an asset is opened from the package.
    /// </summary>
    public event Action<AssetNode, string>? AssetOpened;
    
    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        
        if (ViewModel != null)
        {
            ViewModel.AssetOpened += OnAssetOpened;
        }
    }
    
    protected override void OnUnloaded(RoutedEventArgs e)
    {
        base.OnUnloaded(e);
        
        if (ViewModel != null)
        {
            ViewModel.AssetOpened -= OnAssetOpened;
            ViewModel.Dispose();
        }
    }
    
    private void OnAssetOpened(AssetNode root, string name)
    {
        AssetOpened?.Invoke(root, name);
    }
    
    /// <summary>
    /// Handle Open Package button click - shows file dialog.
    /// </summary>
    private async void OnOpenPackageClick(object? sender, RoutedEventArgs e)
    {
        var settings = SettingsService.Load();
        
        var options = new FilePickerOpenOptions
        {
            AllowMultiple = false,
            Title = "Open DBPF Package",
            SuggestedStartLocation = settings.GetStartFolder(StorageProvider),
            FileTypeFilter =
            [
                new FilePickerFileType("DBPF Packages") { Patterns = ["*.package", "*.dbbf"] },
                new FilePickerFileType("All Files") { Patterns = ["*.*"] }
            ]
        };
        
        var files = await StorageProvider.OpenFilePickerAsync(options);
        if (files == null || files.Count == 0) return;
        
        var path = files[0].TryGetLocalPath();
        if (string.IsNullOrWhiteSpace(path)) return;
        
        settings.LastOpenDirectory = Path.GetDirectoryName(path) ?? settings.LastOpenDirectory;
        SettingsService.Save(settings);
        
        if (ViewModel != null)
        {
            await ViewModel.OpenPackageAsync(path);
        }
    }
    
    /// <summary>
    /// Open a package directly (for use from main window).
    /// </summary>
    public async Task OpenPackageAsync(string path)
    {
        if (ViewModel != null)
        {
            await ViewModel.OpenPackageAsync(path);
        }
    }
}
