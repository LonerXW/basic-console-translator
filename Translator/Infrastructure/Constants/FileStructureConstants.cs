namespace Translator.Infrastructure.Constants
{
    /// <summary>
    /// Contains constants and utility methods for managing file system paths in the application.
    /// </summary>
    public static class FileStructureConstants
    {
        /// <summary>
        /// The name of the directory containing language data.
        /// </summary>
        public const string LANGUAGES_DIR = "Languages";

        /// <summary>
        /// The name of the language configuration file.
        /// </summary>
        public const string LANGUAGES_CONFIG_FILE = "languages.json";

        /// <summary>
        /// The name of the words dictionary file.
        /// </summary>
        public const string WORDS_FILE = "words.json";

        /// <summary>
        /// The name of the phrases dictionary file.
        /// </summary>
        public const string PHRASES_FILE = "phrases.json";

        /// <summary>
        /// Gets the full path to a language directory.
        /// </summary>
        /// <param name="basePath">The base path of the application.</param>
        /// <param name="languageCode">The language code (e.g., "en-uk").</param>
        /// <returns>The full path to the language directory.</returns>
        public static string GetLanguageDirectoryPath(string basePath, string languageCode)
        {
            return Path.Combine(basePath, LANGUAGES_DIR, languageCode);
        }

        /// <summary>
        /// Gets the full path to the languages configuration file.
        /// </summary>
        /// <param name="basePath">The base path of the application.</param>
        /// <returns>The full path to the languages configuration file.</returns>
        public static string GetLanguagesConfigPath(string basePath)
        {
            return Path.Combine(basePath, LANGUAGES_DIR, LANGUAGES_CONFIG_FILE);
        }

        /// <summary>
        /// Gets the full path to a language's words dictionary file.
        /// </summary>
        /// <param name="basePath">The base path of the application.</param>
        /// <param name="languageCode">The language code (e.g., "en-uk").</param>
        /// <returns>The full path to the words dictionary file.</returns>
        public static string GetWordsFilePath(string basePath, string languageCode)
        {
            return Path.Combine(GetLanguageDirectoryPath(basePath, languageCode), WORDS_FILE);
        }

        /// <summary>
        /// Gets the full path to a language's phrases dictionary file.
        /// </summary>
        /// <param name="basePath">The base path of the application.</param>
        /// <param name="languageCode">The language code (e.g., "en-uk").</param>
        /// <returns>The full path to the phrases dictionary file.</returns>
        public static string GetPhrasesFilePath(string basePath, string languageCode)
        {
            return Path.Combine(GetLanguageDirectoryPath(basePath, languageCode), PHRASES_FILE);
        }
    }
} 