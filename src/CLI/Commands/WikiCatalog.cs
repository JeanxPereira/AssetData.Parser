using System.Text;
using ReCap.Parser;

namespace ReCap.Parser.CLI.Commands;

public static class WikiCatalog
{
    private static readonly Dictionary<string, HashSet<string>> _usagesMap = new();

    public static void Run(string outputDir)
    {
        Console.WriteLine($"[WikiGen] Starting documentation generation in: {outputDir}");

        var catalogDir = Path.Combine(outputDir, "Catalog");
        var structsDir = Path.Combine(catalogDir, "Structs");
        var enumsDir = Path.Combine(catalogDir, "Enums");

        Directory.CreateDirectory(structsDir);
        Directory.CreateDirectory(enumsDir);

        var assembly = typeof(AssetCatalog).Assembly;
        var catalogTypes = assembly.GetTypes()
            .Where(t => t.IsSubclassOf(typeof(AssetCatalog)) && !t.IsAbstract)
            .OrderBy(t => t.Name);

        var loadedCatalogs = new List<AssetCatalog>();
        foreach (var type in catalogTypes)
        {
            try
            {
                var instance = (AssetCatalog)Activator.CreateInstance(type)!;
                loadedCatalogs.Add(instance);
            }
            catch
            {
                Console.WriteLine($"[WikiGen] Warning: Could not instantiate catalog {type.Name}");
            }
        }

        BuildDependencyGraph(loadedCatalogs);

        int structCount = 0;
        int enumCount = 0;

        foreach (var catalog in loadedCatalogs)
        {
            foreach (var structName in catalog.StructNames)
            {
                var def = catalog.GetStruct(structName);
                if (def == null) continue;

                var markdown = GenerateMarkdownForStruct(def);
                File.WriteAllText(Path.Combine(structsDir, $"{def.Name}.md"), markdown);
                structCount++;
            }

            foreach (var enumName in catalog.EnumNames)
            {
                var def = catalog.GetEnum(enumName);
                if (def == null) continue;

                var markdown = GenerateMarkdownForEnum(def);
                File.WriteAllText(Path.Combine(enumsDir, $"{def.Name}.md"), markdown);
                enumCount++;
            }
        }

        Console.WriteLine($"[WikiGen] Completed! Generated {structCount} Structs and {enumCount} Enums.");
    }

    private static void BuildDependencyGraph(List<AssetCatalog> catalogs)
    {
        _usagesMap.Clear();

        foreach (var catalog in catalogs)
        {
            foreach (var structName in catalog.StructNames)
            {
                var definition = catalog.GetStruct(structName);
                if (definition == null) continue;

                foreach (var field in definition.Fields)
                {
                    string? dependency = null;

                    if (field.Type == DataType.Struct)
                    {
                        dependency = field.ElementType;
                    }
                    else if (field.Type == DataType.Enum)
                    {
                        dependency = field.EnumType;
                    }
                    else if (field.Type == DataType.Array && field.ElementType != null)
                    {
                        if (!Enum.TryParse<DataType>(field.ElementType, true, out _))
                        {
                            dependency = field.ElementType;
                        }
                    }

                    if (dependency != null)
                    {
                        if (!_usagesMap.ContainsKey(dependency))
                        {
                            _usagesMap[dependency] = new HashSet<string>();
                        }
                        _usagesMap[dependency].Add(structName);
                    }
                }
            }
        }
    }

    private static string GenerateMarkdownForStruct(StructDefinition def)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"# {def.Name}");
        sb.AppendLine($"**Size:** `0x{def.Size:x}`");
        sb.AppendLine($"**Count:** `0x{def.Fields.Count:x}`");
        sb.AppendLine();

        sb.AppendLine("## Structure");
        sb.AppendLine();
        sb.AppendLine("| Offset | DataType | Name |");
        sb.AppendLine("| :--- | :--- | :--- |");

        foreach (var field in def.Fields)
        {
            string offsetHex = $"0x{field.Offset:X2}";
            string typeStr = FormatDataType(field);
            sb.AppendLine($"| `{offsetHex}` | {typeStr} | **{field.Name}** |");
        }
        sb.AppendLine();

        AppendReferencesSection(sb, def.Name);

        return sb.ToString();
    }

    private static string GenerateMarkdownForEnum(EnumDefinition def)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"# {def.Name}");
        sb.AppendLine();

        sb.AppendLine("## Values");
        sb.AppendLine();
        sb.AppendLine("| Value | Name |");
        sb.AppendLine("| :--- | :--- |");

        foreach (var kvp in def.Values.OrderBy(x => x.Key))
        {
            sb.AppendLine($"| `0x{kvp.Key:X8}` | **{kvp.Value}** |");
        }
        sb.AppendLine();

        AppendReferencesSection(sb, def.Name);

        return sb.ToString();
    }

    private static void AppendReferencesSection(StringBuilder sb, string typeName)
    {
        if (_usagesMap.TryGetValue(typeName, out var users) && users.Count > 0)
        {
            sb.AppendLine("---");
            sb.AppendLine("## Reference");
            sb.AppendLine("> Used by the following types:");
            sb.AppendLine();

            foreach (var user in users.OrderBy(u => u))
            {
                sb.AppendLine($"- [`{user}`](../Structs/{user})");
            }
            sb.AppendLine();
        }
    }

    private static string FormatDataType(FieldDefinition field)
    {
        if (field.Type == DataType.Struct)
        {
            var structName = field.ElementType ?? "UnknownStruct";
            return $"[`{structName}`](../Structs/{structName})";
        }

        if (field.Type == DataType.Array)
        {
            var innerType = field.ElementType ?? "Unknown";
            bool isPrimitive = Enum.TryParse<DataType>(innerType, true, out _);

            if (!isPrimitive)
            {
                return $"`Array<`[`{innerType}`](../Structs/{innerType})`>`";
            }
            else
            {
                return $"`Array<{innerType.ToLower()}>`";
            }
        }

        if (field.Type == DataType.Enum)
        {
            return field.EnumType != null
               ? $"[`Enum<{field.EnumType}>`](../Enums/{field.EnumType})"
               : "`Enum`";
        }

        return $"`{field.Type.ToString().ToLower()}`";
    }
}