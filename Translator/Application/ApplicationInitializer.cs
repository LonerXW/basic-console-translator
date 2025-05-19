using System.Reflection;
using Translator.Core.Interfaces;
using Translator.Core.Models;
using Translator.Infrastructure.FileSystem;
using Translator.Services.UI;

namespace Translator.Application
{
    /// <summary>
    /// Handles the initialization of the application, including console setup, path configuration,
    /// and service initialization.
    /// </summary>
    public class ApplicationInitializer
    {
        private readonly string _basePath;
        private readonly IDictionaryLoader _dictionaryLoader;
        private readonly IConsoleUIService _uiService;
        private readonly LanguageConfig _config;

        /// <summary>
        /// Initializes a new instance of the ApplicationInitializer.
        /// Sets up the console encoding, base path, and initializes required services.
        /// </summary>
        public ApplicationInitializer()
        {
            InitializeConsole();
            _basePath = GetBasePath();
            _dictionaryLoader = new DictionaryLoaderService();
            _uiService = new ConsoleUIService();
            _config = _dictionaryLoader.LoadLanguageConfig(_basePath);
        }

        /// <summary>
        /// Gets the base path for the application's data directory.
        /// </summary>
        public string BasePath => _basePath;

        /// <summary>
        /// Gets the dictionary loader service instance.
        /// </summary>
        public IDictionaryLoader DictionaryLoader => _dictionaryLoader;

        /// <summary>
        /// Gets the console UI service instance.
        /// </summary>
        public IConsoleUIService UiService => _uiService;

        /// <summary>
        /// Gets the language configuration containing available language pairs.
        /// </summary>
        public LanguageConfig Config => _config;

        /// <summary>
        /// Initializes the console with UTF-8 encoding for proper character display.
        /// </summary>
        private void InitializeConsole()
        {
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
        }

        /// <summary>
        /// Gets the base path for the application's data directory.
        /// </summary>
        /// <returns>The path to the Data directory relative to the executable.</returns>
        /// <exception cref="Exception">Thrown when the executable directory cannot be determined.</exception>
        private string GetBasePath()
        {
            string exePath = Assembly.GetExecutingAssembly().Location;
            string? exeDirectory = Path.GetDirectoryName(exePath);
            if (string.IsNullOrEmpty(exeDirectory))
            {
                throw new Exception("Cannot determine executable directory");
            }

            return Path.Combine(exeDirectory, "Data");
        }
    }
} 