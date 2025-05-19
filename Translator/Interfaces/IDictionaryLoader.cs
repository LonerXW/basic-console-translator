using Translator.Models;

namespace Translator.Services.Interfaces
{
    public interface IDictionaryLoader
    {
        Dictionary<string, string> LoadDictionary(Language language, string basePath);
        
        LanguageConfig LoadLanguageConfig(string basePath);
    }
} 