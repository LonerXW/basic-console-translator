using System.Text.Json;

namespace Translator.Infrastructure.FileSystem;

public class JsonFileLoader
{
    public T? LoadFromFile<T>(string filePath)
    {
        if (!File.Exists(filePath)) return default;

        var jsonContent = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<T>(jsonContent);
    }
}