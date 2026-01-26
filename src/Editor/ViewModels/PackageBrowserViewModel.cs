using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReCap.Parser;

namespace ReCap.Parser.Editor.ViewModels;

/// <summary>
/// Represents an entry in the DBPF package browser.
/// </summary>
public partial class PackageEntryViewModel : ObservableObject
{
    [ObservableProperty]
    private string _name = string.Empty;
    
    [ObservableProperty]
    private string _type = string.Empty;
    
    [ObservableProperty]
    private int _size;
    
    [ObservableProperty]
    private bool _isCompressed;
    
    /// <summary>The underlying DBPF entry.</summary>
    public DbpfEntry Entry { get; init; }
    
    /// <summary>Full virtual name (name.type).</summary>
    public string FullName => $"{Name}.{Type}";
}

/// <summary>
/// ViewModel for the DBPF Package Browser window.
/// </summary>
public partial class PackageBrowserViewModel : ObservableObject
{
    private DbpfReader? _dbpf;
    private readonly AssetService _assetService;
    
    /// <summary>All entries in the package.</summary>
    public ObservableCollection<PackageEntryViewModel> AllEntries { get; } = [];
    
    /// <summary>Filtered entries based on search/type filter.</summary>
    public ObservableCollection<PackageEntryViewModel> FilteredEntries { get; } = [];
    
    /// <summary>Available type filters.</summary>
    public ObservableCollection<string> TypeFilters { get; } = [];
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsPackageOpen))]
    private string _packagePath = string.Empty;
    
    [ObservableProperty]
    private string _packageName = string.Empty;
    
    [ObservableProperty]
    private int _totalEntries;
    
    [ObservableProperty]
    private string _searchText = string.Empty;
    
    [ObservableProperty]
    private string _selectedTypeFilter = "All";
    
    [ObservableProperty]
    private PackageEntryViewModel? _selectedEntry;
    
    [ObservableProperty]
    private bool _isLoading;
    
    [ObservableProperty]
    private string _statusText = "No package loaded";
    
    public bool IsPackageOpen => _dbpf != null;
    
    /// <summary>Event raised when user wants to open an asset.</summary>
    public event Action<AssetNode, string>? AssetOpened;
    
    public PackageBrowserViewModel() : this(new AssetService()) { }
    
    public PackageBrowserViewModel(AssetService assetService)
    {
        _assetService = assetService;
    }
    
    /// <summary>
    /// Open a DBPF package file.
    /// </summary>
    public async Task OpenPackageAsync(string path)
    {
        if (!File.Exists(path)) return;
        
        IsLoading = true;
        StatusText = "Loading package...";
        
        try
        {
            // Close previous
            _dbpf?.Dispose();
            AllEntries.Clear();
            FilteredEntries.Clear();
            TypeFilters.Clear();
            
            // Open new package
            _dbpf = await Task.Run(() => new DbpfReader(path));
            PackagePath = path;
            PackageName = Path.GetFileName(path);
            TotalEntries = _dbpf.Entries.Count;
            
            // Try to load registries from same directory
            var registryDir = Path.Combine(Path.GetDirectoryName(path) ?? "", "registries");
            if (Directory.Exists(registryDir))
                _dbpf.LoadRegistries(registryDir);
            
            // Build entries list
            var typeSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var entries = new List<PackageEntryViewModel>();
            
            await Task.Run(() =>
            {
                foreach (var assetInfo in _dbpf.ListAssets())
                {
                    var parts = assetInfo.Split('.', 2);
                    var name = parts[0];
                    var type = parts.Length > 1 ? parts[1] : "unknown";
                    
                    typeSet.Add(type);
                    
                    entries.Add(new PackageEntryViewModel
                    {
                        Name = name,
                        Type = type
                    });
                }
            });
            
            // Add to UI on UI thread
            foreach (var entry in entries)
                AllEntries.Add(entry);
            
            // Build type filters
            TypeFilters.Add("All");
            foreach (var type in typeSet.OrderBy(t => t))
                TypeFilters.Add(type);
            
            SelectedTypeFilter = "All";
            ApplyFilter();
            
            StatusText = $"Loaded {TotalEntries:N0} entries";
        }
        catch (Exception ex)
        {
            StatusText = $"Error: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }
    
    /// <summary>
    /// Open the selected asset for editing.
    /// </summary>
    [RelayCommand]
    public async Task OpenSelectedAssetAsync()
    {
        if (_dbpf == null || SelectedEntry == null) return;
        
        IsLoading = true;
        StatusText = $"Loading {SelectedEntry.FullName}...";
        
        try
        {
            var data = _dbpf.GetAsset(SelectedEntry.FullName);
            if (data == null)
            {
                StatusText = "Failed to read asset data";
                return;
            }
            
            var fileType = _assetService.GetFileType(SelectedEntry.Type);
            if (fileType == null)
            {
                StatusText = $"Unknown asset type: {SelectedEntry.Type}";
                return;
            }
            
            var root = await Task.Run(() => 
                _assetService.Parser.Parse(data, fileType.RootStruct, fileType.HeaderSize));
            
            AssetOpened?.Invoke(root, SelectedEntry.FullName);
            StatusText = $"Opened: {SelectedEntry.FullName}";
        }
        catch (Exception ex)
        {
            StatusText = $"Error: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }
    
    /// <summary>
    /// Apply search and type filter.
    /// </summary>
    private void ApplyFilter()
    {
        FilteredEntries.Clear();
        
        var query = AllEntries.AsEnumerable();
        
        // Type filter
        if (SelectedTypeFilter != "All")
            query = query.Where(e => e.Type.Equals(SelectedTypeFilter, StringComparison.OrdinalIgnoreCase));
        
        // Search filter
        if (!string.IsNullOrWhiteSpace(SearchText))
            query = query.Where(e => e.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
        
        foreach (var entry in query.OrderBy(e => e.Name))
            FilteredEntries.Add(entry);
        
        StatusText = $"Showing {FilteredEntries.Count:N0} of {TotalEntries:N0} entries";
    }
    
    partial void OnSearchTextChanged(string value) => ApplyFilter();
    partial void OnSelectedTypeFilterChanged(string value) => ApplyFilter();
    
    public void Dispose()
    {
        _dbpf?.Dispose();
    }
}
