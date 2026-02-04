using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AssetData.Parser;

namespace AssetData.Parser.Editor.ViewModels;

/// <summary>
/// Main ViewModel for the asset editor. Uses CommunityToolkit.Mvvm for clean MVVM pattern.
/// Directly consumes AssetNode from Core - no XML bridge!
/// </summary>
public partial class MainViewModel : ObservableObject
{
    private readonly AssetService _assetService;
    
    /// <summary>Root nodes of the current asset tree.</summary>
    public ObservableCollection<AssetNode> Roots { get; } = [];
    
    /// <summary>Suggestions for key/asset autocomplete.</summary>
    public ObservableCollection<string> KeyAssetSuggestions { get; } = [];
    
    /// <summary>Flattened node list for search (cached).</summary>
    private List<AssetNode>? _flatCache;
    private int _searchIndex = -1;
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsSelectedEditable))]
    [NotifyPropertyChangedFor(nameof(SelectedDisplayValue))]
    private AssetNode? _selected;
    
    [ObservableProperty]
    private string _currentFilePath = string.Empty;
    
    [ObservableProperty]
    private bool _isDirty;
    
    // Search & Replace
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanFind))]
    private string _findText = string.Empty;
    
    [ObservableProperty]
    private string _replaceText = string.Empty;
    
    [ObservableProperty]
    private bool _searchInKeys = true;
    
    [ObservableProperty]
    private bool _searchInValues = true;
    
    [ObservableProperty]
    private bool _caseSensitive;
    
    // Computed properties
    public bool IsSelectedEditable => Selected?.IsEditable ?? false;
    public string SelectedDisplayValue => Selected?.DisplayValue ?? string.Empty;
    public bool CanFind => !string.IsNullOrEmpty(FindText);
    
    public MainViewModel() : this(new AssetService()) { }
    
    public MainViewModel(AssetService assetService)
    {
        _assetService = assetService;
    }
    
    /// <summary>
    /// Load an asset file directly using the Core API.
    /// No XML intermediary - direct parsing to AssetNode!
    /// </summary>
    [RelayCommand]
    public async Task LoadFileAsync(string filePath)
    {
        try
        {
            // Direct API call - no process spawn, no XML file!
            var root = await Task.Run(() => _assetService.LoadFile(filePath));
            
            SetRoot(root);
            CurrentFilePath = filePath;
            IsDirty = false;
            
            // Build suggestions from asset paths
            BuildSuggestions();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to load asset: {ex.Message}", ex);
        }
    }
    
    /// <summary>
    /// Set the root node(s) of the tree.
    /// </summary>
    public void SetRoot(AssetNode root)
    {
        Roots.Clear();
        Roots.Add(root);
        _flatCache = null;
        Selected = null;
    }
    
    /// <summary>
    /// Export current asset to XML file.
    /// </summary>
    [RelayCommand]
    public async Task ExportXmlAsync(string outputPath)
    {
        if (Roots.Count == 0) return;
        
        await Task.Run(() => _assetService.ExportXml(Roots[0], outputPath));
    }
    
    /// <summary>
    /// Find next matching node.
    /// </summary>
    [RelayCommand(CanExecute = nameof(CanFind))]
    public void FindNext()
    {
        if (string.IsNullOrEmpty(FindText)) return;
        EnsureFlatCache();
        if (_flatCache == null || _flatCache.Count == 0) return;
        
        var comparison = CaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
        int start = (_searchIndex + 1) % _flatCache.Count;
        int i = start;
        
        do
        {
            if (MatchNode(_flatCache[i], comparison))
            {
                _searchIndex = i;
                Selected = _flatCache[i];
                return;
            }
            i = (i + 1) % _flatCache.Count;
        } while (i != start);
    }
    
    /// <summary>
    /// Find previous matching node.
    /// </summary>
    [RelayCommand(CanExecute = nameof(CanFind))]
    public void FindPrev()
    {
        if (string.IsNullOrEmpty(FindText)) return;
        EnsureFlatCache();
        if (_flatCache == null || _flatCache.Count == 0) return;
        
        var comparison = CaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
        int start = (_searchIndex - 1 + _flatCache.Count) % _flatCache.Count;
        int i = start;
        
        do
        {
            if (MatchNode(_flatCache[i], comparison))
            {
                _searchIndex = i;
                Selected = _flatCache[i];
                return;
            }
            i = (i - 1 + _flatCache.Count) % _flatCache.Count;
        } while (i != start);
    }
    
    /// <summary>
    /// Replace current match.
    /// </summary>
    [RelayCommand]
    public void ReplaceOne()
    {
        if (Selected == null || string.IsNullOrEmpty(FindText)) return;
        
        var comparison = CaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
        
        if (SearchInKeys && Selected.Name.Contains(FindText, comparison))
        {
            Selected.Name = ReplaceFirst(Selected.Name, FindText, ReplaceText, comparison);
            IsDirty = true;
        }
        
        if (SearchInValues && Selected is StringNode sn && sn.Value.Contains(FindText, comparison))
        {
            sn.Value = ReplaceFirst(sn.Value, FindText, ReplaceText, comparison);
            IsDirty = true;
        }
    }
    
    /// <summary>
    /// Replace all matches.
    /// </summary>
    [RelayCommand]
    public void ReplaceAll()
    {
        if (string.IsNullOrEmpty(FindText)) return;
        EnsureFlatCache();
        if (_flatCache == null) return;
        
        var comparison = CaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
        
        foreach (var node in _flatCache)
        {
            if (SearchInKeys && node.Name.Contains(FindText, comparison))
            {
                node.Name = ReplaceAll(node.Name, FindText, ReplaceText, comparison);
                IsDirty = true;
            }
            
            if (SearchInValues && node is StringNode sn && sn.Value.Contains(FindText, comparison))
            {
                sn.Value = ReplaceAll(sn.Value, FindText, ReplaceText, comparison);
                IsDirty = true;
            }
        }
    }
    
    /// <summary>
    /// Get the schema for the currently selected struct node.
    /// </summary>
    public StructDefinition? GetSelectedSchema()
    {
        if (Selected is StructNode sn)
            return _assetService.GetStructSchema(sn.TypeName);
        return null;
    }
    
    /// <summary>
    /// Get enum values for the selected enum node.
    /// </summary>
    public EnumDefinition? GetSelectedEnumSchema()
    {
        if (Selected is EnumNode en)
            return _assetService.GetEnumSchema(en.EnumType);
        return null;
    }
    
    private void EnsureFlatCache()
    {
        if (_flatCache != null) return;
        _flatCache = [];
        foreach (var root in Roots)
            _flatCache.AddRange(_assetService.Flatten(root));
        _searchIndex = -1;
    }
    
    private bool MatchNode(AssetNode node, StringComparison comparison)
    {
        if (SearchInKeys && node.Name.Contains(FindText, comparison))
            return true;
        
        if (SearchInValues)
        {
            return node switch
            {
                StringNode sn => sn.Value.Contains(FindText, comparison),
                NumberNode nn => nn.DisplayValue.Contains(FindText, comparison),
                BooleanNode bn => bn.DisplayValue.Contains(FindText, comparison),
                EnumNode en => en.DisplayValue.Contains(FindText, comparison),
                _ => false
            };
        }
        
        return false;
    }
    
    private void BuildSuggestions()
    {
        KeyAssetSuggestions.Clear();
        EnsureFlatCache();
        
        if (_flatCache == null) return;
        
        var paths = _flatCache
            .OfType<StringNode>()
            .Where(n => n.NodeKind == AssetNodeKind.Asset && !string.IsNullOrWhiteSpace(n.Value))
            .Select(n => n.Value)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(x => x);
        
        foreach (var path in paths)
            KeyAssetSuggestions.Add(path);
    }
    
    private static string ReplaceFirst(string source, string find, string replace, StringComparison comparison)
    {
        int index = source.IndexOf(find, comparison);
        if (index < 0) return source;
        return string.Concat(source.AsSpan(0, index), replace, source.AsSpan(index + find.Length));
    }
    
    private static string ReplaceAll(string source, string find, string replace, StringComparison comparison)
    {
        if (string.IsNullOrEmpty(find)) return source;
        
        int start = 0;
        while (true)
        {
            int index = source.IndexOf(find, start, comparison);
            if (index < 0) break;
            source = string.Concat(source.AsSpan(0, index), replace, source.AsSpan(index + find.Length));
            start = index + replace.Length;
        }
        return source;
    }
    
    partial void OnFindTextChanged(string value)
    {
        _flatCache = null; // Invalidate cache when search text changes
        FindNextCommand.NotifyCanExecuteChanged();
        FindPrevCommand.NotifyCanExecuteChanged();
    }
}
