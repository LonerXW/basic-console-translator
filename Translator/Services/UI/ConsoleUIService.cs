using Translator.Core.Interfaces;
using Translator.Core.Models;
using Translator.Infrastructure.Constants;

namespace Translator.Services.UI
{
    /// <summary>
    /// Provides console-based user interface functionality for the translation application.
    /// Implements the IConsoleUIService interface to handle user interactions and display information.
    /// </summary>
    public class ConsoleUIService : IConsoleUIService
    {
        /// <summary>
        /// Displays a message to the user using the MenuManager.
        /// </summary>
        /// <param name="message">The message to display.</param>
        public void DisplayMessage(string message)
        {
            MenuManager.ShowTranslationResult(message);
        }

        /// <summary>
        /// Gets user input from the console, trimming any whitespace.
        /// </summary>
        /// <returns>The trimmed user input, or an empty string if null.</returns>
        public string GetUserInput()
        {
            return Console.ReadLine()?.Trim() ?? string.Empty;
        }

        /// <summary>
        /// Clears the console screen using ANSI escape sequences if available,
        /// falling back to Console.Clear() if ANSI is not supported.
        /// </summary>
        public void ClearScreen()
        {
            // ANSI escape sequence for clearing the screen
            try
            {
                Console.Write("\u001b[2J\u001b[H");
                Console.Out.Flush();
            }
            catch
            {
                // Fallback for environments where ANSI is not supported
                Console.Clear();
            }
        }

        /// <summary>
        /// Handles the language selection process, displaying available languages and processing user input.
        /// </summary>
        /// <param name="config">The language configuration containing available language pairs.</param>
        /// <returns>The selected language pair.</returns>
        /// <exception cref="OperationCanceledException">Thrown when the user chooses to exit the selection.</exception>
        public Language SelectLanguage(LanguageConfig config)
        {
            MenuManager.ShowLanguageList(config);
            while (true)
            {
                MenuManager.ShowLanguageSelectionPrompt(config.AvailableLanguages.Count);
                string? input = Console.ReadLine()?.Trim();

                if (input?.ToLower() == "exit")
                {
                    throw new OperationCanceledException();
                }

                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= config.AvailableLanguages.Count)
                {
                    return config.AvailableLanguages[choice - 1];
                }

                MenuManager.ShowInvalidChoice();
            }
        }

        /// <summary>
        /// Runs the main translation loop, handling user input and displaying translation results.
        /// </summary>
        /// <param name="translator">The translation service to use for translating text.</param>
        /// <returns>True if the user wants to return to the main menu, false if they want to change language.</returns>
        public bool RunTranslationLoop(ITranslationService translator)
        {
            while (true)
            {
                MenuManager.ShowTranslationPrompt();
                string? input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input))
                {
                    MenuManager.ShowEmptyInputError();
                    continue;
                }
                if (input.ToLower() == "exit")
                {
                    return true;
                }
                
                try
                {
                    string translation = translator.Translate(input);
                    MenuManager.ShowTranslationResult(translation);

                    MenuManager.ShowAfterTranslationMenu();
                    string? choice = Console.ReadLine()?.Trim().ToLower();
                    if (choice == "1")
                    {
                        return true;
                    }
                    if (choice == "2")
                    {
                        Environment.Exit(0);
                    }
                    if (choice == "3")
                    {
                        return false;
                    }
                    if (choice == "4")
                    {
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    MenuManager.ShowTranslationError(ex.Message);
                }
            }
        }
    }
} 