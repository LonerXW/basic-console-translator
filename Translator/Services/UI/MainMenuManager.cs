using Translator.Core.Interfaces;
using Translator.Core.Models;
using Translator.Services.Translation;

namespace Translator.Services.UI;

/// <summary>
///     Manages the main menu flow and user interactions in the translation application.
///     Handles the coordination between different parts of the application based on user choices.
/// </summary>
public class MainMenuManager
{
    private readonly string _basePath;
    private readonly LanguageConfig _config;
    private readonly IDictionaryLoader _dictionaryLoader;
    private readonly IConsoleUIService _uiService;

    /// <summary>
    ///     Initializes a new instance of the MainMenuManager with required services and configuration.
    /// </summary>
    /// <param name="uiService">The console UI service for user interaction.</param>
    /// <param name="dictionaryLoader">The service for loading translation dictionaries.</param>
    /// <param name="basePath">The base path for dictionary files.</param>
    /// <param name="config">The language configuration containing available language pairs.</param>
    public MainMenuManager(IConsoleUIService uiService, IDictionaryLoader dictionaryLoader, string basePath,
        LanguageConfig config)
    {
        _uiService = uiService;
        _dictionaryLoader = dictionaryLoader;
        _basePath = basePath;
        _config = config;
    }

    /// <summary>
    ///     Handles the user's menu choice and executes the corresponding action.
    /// </summary>
    /// <param name="input">The user's input choice.</param>
    /// <returns>True if the application should continue running, false if it should exit.</returns>
    public bool HandleUserChoice(string? input)
    {
        if (input == "exit" || input == "3")
        {
            MenuManager.ShowTranslationError("Goodbye!");
            return false;
        }

        if (input == "start" || input == "1")
            HandleTranslationMode();
        else if (input == "help" || input == "2")
            HandleHelpMode();
        else
            MenuManager.ShowInvalidChoice();

        return true;
    }

    /// <summary>
    ///     Handles the translation mode, including language selection and translation loop.
    /// </summary>
    private void HandleTranslationMode()
    {
        var stayInLanguageSelection = true;

        while (stayInLanguageSelection)
            try
            {
                _uiService.ClearScreen();

                var selectedLanguage = _uiService.SelectLanguage(_config);

                _uiService.ClearScreen();

                var dictionary = _dictionaryLoader.LoadDictionary(selectedLanguage, _basePath);
                ITranslationService translator = new TranslationService(dictionary, selectedLanguage);

                var continueApp = _uiService.RunTranslationLoop(translator);

                if (continueApp) stayInLanguageSelection = false;
            }
            catch (OperationCanceledException)
            {
                stayInLanguageSelection = false;
            }

        _uiService.ClearScreen();
    }

    /// <summary>
    ///     Handles the help mode, displaying help information and processing user navigation choices.
    /// </summary>
    private void HandleHelpMode()
    {
        _uiService.ClearScreen();
        MenuManager.ShowHelp();

        var validChoice = false;
        while (!validChoice)
        {
            MenuManager.ShowTranslationPrompt();
            var choice = Console.ReadLine()?.Trim();

            if (choice == "2" || choice?.ToLower() == "exit")
            {
                MenuManager.ShowTranslationError("Goodbye!");
                Environment.Exit(0);
            }
            else if (choice == "1" || choice?.ToLower() == "menu" || choice?.ToLower() == "back")
            {
                validChoice = true;
            }
            else
            {
                MenuManager.ShowInvalidChoice();
            }
        }

        _uiService.ClearScreen();
    }
}