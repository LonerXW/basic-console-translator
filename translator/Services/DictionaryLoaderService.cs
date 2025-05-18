using Translator.Models;
using Translator.Services.Interfaces;
using System.Text.Json;

namespace Translator.Services
{
    /// Сервіс для завантаження словників з JSON-файлів
    public class DictionaryLoaderService : IDictionaryLoader
    {
        private readonly bool _debugMode = false;
        
        public Dictionary<string, string> LoadDictionary(Language language, string basePath)
        {
            Dictionary<string, string> dictionary = new(StringComparer.OrdinalIgnoreCase);
            
            // Перевірка коректності коду мови
            string[] parts = language.Code.Split('-');
            if (parts.Length != 2)
            {
                throw new ArgumentException($"Invalid language code format: {language.Code}");
            }
            
            string directCode = language.Code;
            string languagesDir = Path.Combine(basePath, "Languages");
            string directDir = Path.Combine(languagesDir, directCode);
            
            if (!Directory.Exists(directDir))
            {
                throw new Exception($"Could not find dictionary for language pair {language.Code}");
            }
            
            LoadDirectDictionary(directDir, dictionary);
            
            if (dictionary.Count == 0)
            {
                throw new Exception($"Could not load any dictionary for language pair {language.Code}");
            }
            
            return dictionary;
        }
        
        /// Завантажує словники слів та фраз із вказаної директорії
        private void LoadDirectDictionary(string directoryPath, Dictionary<string, string> dictionary)
        {
            string phrasesFile = Path.Combine(directoryPath, "phrases.json");
            if (File.Exists(phrasesFile))
            {
                var phrasesDict = LoadJsonDictionary(phrasesFile, "phrases");
                if (phrasesDict != null)
                {
                    foreach (var item in phrasesDict)
                    {
                        if (item.Key == null || item.Value == null) continue;
                        string key = item.Key.ToLowerInvariant().Trim();
                        string value = item.Value.ToLowerInvariant().Trim();
                        dictionary[key] = value;
                    }
                }
            }
            
            string wordsFile = Path.Combine(directoryPath, "words.json");
            if (File.Exists(wordsFile))
            {
                var wordsDict = LoadJsonDictionary(wordsFile, "words");
                if (wordsDict != null)
                {
                    foreach (var item in wordsDict)
                    {
                        if (item.Key == null || item.Value == null) continue;
                        string key = item.Key.ToLowerInvariant().Trim();
                        string value = item.Value.ToLowerInvariant().Trim();
                        dictionary[key] = value;
                    }
                }
            }
        }
        
        /// Десеріалізує JSON-файл у словник
        private Dictionary<string, string>? LoadJsonDictionary(string filePath, string fileType)
        {
            try
            {
                string json = File.ReadAllText(filePath, System.Text.Encoding.UTF8);
                if (string.IsNullOrWhiteSpace(json))
                {
                    return null;
                }
                
                var options = new JsonSerializerOptions
                {
                    AllowTrailingCommas = true,
                    ReadCommentHandling = JsonCommentHandling.Skip
                };
                
                return JsonSerializer.Deserialize<Dictionary<string, string>>(json, options);
            }
            catch
            {
                return null;
            }
        }
        
        public LanguageConfig LoadLanguageConfig(string basePath)
        {
            try
            {
                string configPath = Path.Combine(basePath, "Languages", "languages.json");
                if (File.Exists(configPath))
                {
                    string json = File.ReadAllText(configPath);
                    var config = JsonSerializer.Deserialize<LanguageConfig>(json);
                    return config ?? new LanguageConfig();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading language config: {ex.Message}");
            }

            return new LanguageConfig { AvailableLanguages = new List<Language>() };
        }
    }
} 