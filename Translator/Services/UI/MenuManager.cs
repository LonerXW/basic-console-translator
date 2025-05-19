using Translator.Infrastructure.Constants;
using Translator.Core.Models;

namespace Translator.Services.UI
{
    /// <summary>
    /// Provides centralized management of all console UI messages and menus in the application.
    /// </summary>
    public static class MenuManager
    {
        /// <summary>
        /// Displays the main menu options to the user.
        /// </summary>
        public static void ShowMainMenu()
        {
            Console.WriteLine($"\n{UIConstants.Emojis.MENU} Main Menu:");
            Console.WriteLine("1. Start Translation");
            Console.WriteLine("2. Help");
            Console.WriteLine("3. Exit");
        }

        /// <summary>
        /// Displays the help information and usage tips to the user.
        /// </summary>
        public static void ShowHelp()
        {
            Console.WriteLine($"\n{UIConstants.Emojis.HELP} Help:");
            Console.WriteLine("1. Select a language pair for translation");
            Console.WriteLine("2. Enter text to translate");
            Console.WriteLine("3. View the translation result");
            Console.WriteLine("4. Enter 'exit' to return to the main menu");
            Console.WriteLine("\nTips:");
            Console.WriteLine("- You can use numbers or text commands (e.g., '1' or 'start')");
            Console.WriteLine("- Empty input is not allowed");
            Console.WriteLine("- Unknown words will be kept as is");
            Console.WriteLine("- Punctuation marks are preserved");
            Console.WriteLine("- Capitalization is maintained");
        }

        /// <summary>
        /// Displays the translation result to the user.
        /// </summary>
        /// <param name="translation">The translated text to display.</param>
        public static void ShowTranslationResult(string translation)
        {
            Console.WriteLine($"\n{UIConstants.Emojis.TRANSLATION} Translation: {translation}");
        }

        /// <summary>
        /// Displays the prompt for entering text to translate.
        /// </summary>
        public static void ShowTranslationPrompt()
        {
            Console.Write($"\n{UIConstants.Emojis.THINKING} Enter text to translate (or 'exit' to return, or 'menu' то back at menu):");
        }
        
        /// <summary>
        /// Displays a translation-related error message to the user.
        /// </summary>
        /// <param name="message">The error message to display.</param>
        public static void ShowTranslationError(string message)
        {
            Console.WriteLine($"\n{UIConstants.Emojis.ERROR} Error: {message}");
        }

        /// <summary>
        /// Displays the menu options available after a translation is completed.
        /// </summary>
        public static void ShowAfterTranslationMenu()
        {
            Console.WriteLine("\n1. Back to menu");
            Console.WriteLine("2. Exit");
            Console.WriteLine("3. Change language");
            Console.WriteLine("4. Stay on current language");
        }

        /// <summary>
        /// Displays the prompt for selecting a language pair.
        /// </summary>
        /// <param name="count">The total number of available language pairs.</param>
        public static void ShowLanguageSelectionPrompt(int count)
        {
            Console.Write($"\n{UIConstants.Emojis.THINKING} Select language (1-{count}) or 'exit' to return: ");
        }

        /// <summary>
        /// Displays a message indicating an invalid user choice.
        /// </summary>
        public static void ShowInvalidChoice()
        {
            Console.WriteLine($"{UIConstants.Emojis.ERROR} Invalid choice. Please try again.");
        }

        /// <summary>
        /// Displays a message indicating that empty input is not allowed.
        /// </summary>
        public static void ShowEmptyInputError()
        {
            Console.WriteLine($"{UIConstants.Emojis.ERROR} Input cannot be empty.");
        }

        /// <summary>
        /// Displays the list of available language pairs.
        /// </summary>
        /// <param name="config">The language configuration containing available language pairs.</param>
        public static void ShowLanguageList(LanguageConfig config)
        {
            Console.WriteLine($"\n{UIConstants.Emojis.LANGUAGE} Available languages:");
            for (int i = 0; i < config.AvailableLanguages.Count; i++)
            {
                var lang = config.AvailableLanguages[i];
                Console.WriteLine($"{i + 1}. {lang.SourceFlag} {lang.SourceLanguage} → {lang.TargetFlag} {lang.TargetLanguage}");
            }
        }

        /// <summary>
        /// Displays a general error message to the user.
        /// </summary>
        /// <param name="message">The error message to display.</param>
        public static void ShowError(string message)
        {
            Console.WriteLine($"{UIConstants.Emojis.ERROR} Error: {message}");
        }

        /// <summary>
        /// Displays the executable path for debugging purposes.
        /// </summary>
        /// <param name="path">The path to display.</param>
        public static void ShowExecutablePath(string path)
        {
            Console.WriteLine($"Executable path: {path}");
        }

        /// <summary>
        /// Displays a prompt asking the user to press any key to exit.
        /// </summary>
        public static void ShowPressAnyKeyToExit()
        {
            Console.WriteLine("\nPress any key to exit...");
        }

        /// <summary>
        /// Displays a prompt asking the user to select an action.
        /// </summary>
        public static void ShowActionPrompt()
        {
            Console.Write($"\n{UIConstants.Emojis.THINKING} Select an action: ");
        }
    }
} 