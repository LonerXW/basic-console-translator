using Translator.Core.Models;

namespace Translator.Core.Interfaces;

/// <summary>
///     Defines the interface for loading dictionaries and language configurations from the file system.
/// </summary>
public interface IDictionaryLoader
{
    Dictionary<string, string> LoadDictionary(Language language, string basePath);

    LanguageConfig LoadLanguageConfig(string basePath);
}