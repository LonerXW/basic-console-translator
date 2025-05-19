using Translator.Models;
using Translator.Services.Interfaces;

namespace Translator.Services
{
    /// Сервіс для роботи з консольним інтерфейсом користувача
    public class ConsoleUIService : IConsoleUIService
    {
        private const string TRANSLATE_EMOJI = "🌐";
        private const string THINKING_EMOJI = "🤔";
        private const string EXIT_EMOJI = "👋";
        private const string ERROR_EMOJI = "❌";
        private const string SETTINGS_EMOJI = "⚙️";
        
        public void ShowWelcomeMessage()
        {
            Console.WriteLine("\nWelcome to Multi-Language Translator!");
            Console.WriteLine("Type 'exit' to quit the program");
            Console.WriteLine("Type 'menu' or 'back' to return to language selection");
            Console.WriteLine("----------------------------------------");
        }
        
        public Language SelectLanguage(LanguageConfig config)
        {
            if (config.AvailableLanguages.Count == 0)
                throw new Exception("No languages available");
            
            if (config.AvailableLanguages.Count == 1)
                return config.AvailableLanguages[0];
            
            var availablePairs = config.AvailableLanguages
                .OrderBy(l => l.Name)
                .ToList();

            Console.WriteLine($"\n{SETTINGS_EMOJI} Select a language pair:");
            
            for (int i = 0; i < availablePairs.Count; i++)
            {
                Console.WriteLine($"{i + 1,2}. {availablePairs[i].DisplayName}");
            }
            
            Console.WriteLine("\nEnter 'menu' or 'back' to return to main menu, 'exit' to quit");
            
            while (true)
            {
                Console.Write($"\n{THINKING_EMOJI} Select language (1-{availablePairs.Count}): ");
                string? input = Console.ReadLine();

                if (input?.ToLower() == "back" || input?.ToLower() == "menu")
                {
                    throw new OperationCanceledException("Return to main menu");
                }
                
                if (input?.ToLower() == "exit")
                {
                    Console.WriteLine($"\n{EXIT_EMOJI} Goodbye!");
                    Environment.Exit(0); // Негайний вихід з програми
                }

                if (int.TryParse(input, out int choice) &&
                    choice >= 1 && choice <= availablePairs.Count)
                {
                    return availablePairs[choice - 1];
                }

                Console.WriteLine($"{ERROR_EMOJI} Invalid choice. Please try again.");
            }
        }
        
        /// Запускає цикл перекладу для вибраної мовної пари
        public bool RunTranslationLoop(ITranslationService translationService)
        {
            var language = translationService.CurrentLanguage;
            
            Console.WriteLine($"\n{TRANSLATE_EMOJI} Selected: {language.DisplayName}");
            Console.WriteLine($"Enter text in {language.SourceLanguage} or {language.TargetLanguage}");
            Console.WriteLine("Type 'menu' or 'back' to return to main menu, 'exit' to quit");

            while (true)
            {
                Console.Write($"\n{THINKING_EMOJI} Enter text to translate: ");
                string? input = Console.ReadLine()?.Trim();
                
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine($"{ERROR_EMOJI} Please enter some text");
                    continue;
                }
                
                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"\n{EXIT_EMOJI} Goodbye!");
                    return false;
                }
                
                if (input.Equals("menu", StringComparison.OrdinalIgnoreCase) ||
                    input.Equals("back", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }

                try
                {
                    string translation = translationService.Translate(input);
                    
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{TRANSLATE_EMOJI} {translation}");
                    Console.ResetColor();
                    
                    Console.WriteLine("\n1. Continue with current language");
                    Console.WriteLine("2. Return to main menu");
                    Console.Write("Select option (1/2): ");

                    string? choice = Console.ReadLine()?.Trim();
                    
                    if (choice == "2" || choice?.Equals("menu", StringComparison.OrdinalIgnoreCase) == true)
                    {
                        return true;
                    }

                    ClearConsole();
                    Console.WriteLine($"{TRANSLATE_EMOJI} Selected: {language.DisplayName}");
                    Console.WriteLine($"Enter text in {language.SourceLanguage} or {language.TargetLanguage}");
                    Console.WriteLine("Type 'menu' or 'back' to return to main menu, 'exit' to quit");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ERROR_EMOJI} Translation error: {ex.Message}");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    ClearConsole();
                    Console.WriteLine($"{TRANSLATE_EMOJI} Selected: {language.DisplayName}");
                    Console.WriteLine($"Enter text in {language.SourceLanguage} or {language.TargetLanguage}");
                    Console.WriteLine("Type 'menu' or 'back' to return to main menu, 'exit' to quit");
                }
            }
        }
        
        public void ClearConsole()
        {
            try
            {
                Console.Clear();
            }
            catch (Exception)
            {
                try
                {
                    // Альтернативний спосіб очищення екрану через ANSI-послідовності
                    Console.Write("\u001b[2J\u001b[H");
                }
                catch
                {
                    
                }
            }
        }
    }
} 