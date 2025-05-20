using System.Reflection;
using System.Text;
using Translator.Core.Interfaces;
using Translator.Core.Models;
using Translator.Infrastructure.FileSystem;
using Translator.Services.UI;

namespace Translator.Application;

/// <summary>
///     Handles the initialization of the application, including console setup, path configuration,
///     and service initialization.
/// </summary>
public class ApplicationInitializer
{
    /// <summary>
    ///     Initializes a new instance of the ApplicationInitializer.
    ///     Sets up the console encoding, base path, and initializes required services.
    /// </summary>
    public ApplicationInitializer()
    {
        InitializeConsole();
        BasePath = GetBasePath();
        DictionaryLoader = new DictionaryLoaderService();
        UiService = new ConsoleUIService();
        Config = DictionaryLoader.LoadLanguageConfig(BasePath);
    }

    /// <summary>
    ///     Gets the base path for the application's data directory.
    /// </summary>
    public string BasePath { get; }

    /// <summary>
    ///     Gets the dictionary loader service instance.
    /// </summary>
    public IDictionaryLoader DictionaryLoader { get; }

    /// <summary>
    ///     Gets the console UI service instance.
    /// </summary>
    public IConsoleUIService UiService { get; }

    /// <summary>
    ///     Gets the language configuration containing available language pairs.
    /// </summary>
    public LanguageConfig Config { get; }

    /// <summary>
    ///     Initializes the console with UTF-8 encoding for proper character display.
    /// </summary>
    private void InitializeConsole()
    {
        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;
    }

    /// <summary>
    ///     Gets the base path for the application's data directory.
    /// </summary>
    /// <returns>The path to the Data directory relative to the executable.</returns>
    /// <exception cref="Exception">Thrown when the executable directory cannot be determined.</exception>
    private string GetBasePath()
    {
        var exePath = Assembly.GetExecutingAssembly().Location;
        var exeDirectory = Path.GetDirectoryName(exePath);
        if (string.IsNullOrEmpty(exeDirectory)) throw new Exception("Cannot determine executable directory");

        return Path.Combine(exeDirectory, "Data");
    }
}