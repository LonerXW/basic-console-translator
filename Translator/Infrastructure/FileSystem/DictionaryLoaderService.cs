using Translator.Core.Interfaces;
using Translator.Core.Models;
using Translator.Infrastructure.Constants;

namespace Translator.Infrastructure.FileSystem
{
    /// <summary>
    /// Service for loading dictionaries and language configurations from the file system.
    /// </summary>
    public class DictionaryLoaderService : IDictionaryLoader
    {
        private readonly JsonFileLoader _jsonLoader;

        /// <summary>
        /// Initializes a new instance of the DictionaryLoaderService.
        /// </summary>
        public DictionaryLoaderService()
        {
            _jsonLoader = new JsonFileLoader();
        }

        /// <summary>
        /// Loads a dictionary for the specified language by combining words and phrases from corresponding JSON files.
        /// </summary>
        /// <param name="language">The language for which to load the dictionary.</param>
        /// <param name="basePath">The base path to the data directory.</param>
        /// <returns>A dictionary containing all words and phrases for the specified language.</returns>
        /// <exception cref="Exception">Thrown when the language directory is not found.</exception>
        public Dictionary<string, string> LoadDictionary(Language language, string basePath)
        {
            var langDir = FileStructureConstants.GetLanguageDirectoryPath(basePath, language.Code);
            if (!Directory.Exists(langDir))
            {
                throw new Exception($"Language directory not found: {langDir}");
            }

            var dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            // Load words
            var wordsPath = FileStructureConstants.GetWordsFilePath(basePath, language.Code);
            var words = _jsonLoader.LoadFromFile<Dictionary<string, string>>(wordsPath);
            if (words != null)
            {
                foreach (var (key, value) in words)
                {
                    dictionary[key] = value;
                }
            }

            // Load phrases
            var phrasesPath = FileStructureConstants.GetPhrasesFilePath(basePath, language.Code);
            var phrases = _jsonLoader.LoadFromFile<Dictionary<string, string>>(phrasesPath);
            if (phrases != null)
            {
                foreach (var (key, value) in phrases)
                {
                    dictionary[key] = value;
                }
            }

            return dictionary;
        }

        /// <summary>
        /// Loads the language configuration from a JSON file.
        /// </summary>
        /// <param name="basePath">The base path to the data directory.</param>
        /// <returns>The language configuration containing a list of available language pairs.</returns>
        /// <exception cref="FileNotFoundException">Thrown when the configuration file is not found.</exception>
        public LanguageConfig LoadLanguageConfig(string basePath)
        {
            var configPath = FileStructureConstants.GetLanguagesConfigPath(basePath);
            var config = _jsonLoader.LoadFromFile<LanguageConfig>(configPath);
            
            if (config == null)
            {
                throw new FileNotFoundException($"Language configuration file not found at {configPath}");
            }

            return config;
        }
    }
} 