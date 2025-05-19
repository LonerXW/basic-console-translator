using Translator.Models;
using Translator.Services;
using Translator.Services.Interfaces;
using System.Reflection;

namespace Translator
{
    class Program
    {
        private const string TRANSLATE_EMOJI = "🌐";
        private const string THINKING_EMOJI = "🤔";
        private const string EXIT_EMOJI = "👋";
        private const string ERROR_EMOJI = "❌";
        private const string SETTINGS_EMOJI = "⚙️";

        public static void Main()
        {
            try
            {
                Console.InputEncoding = System.Text.Encoding.UTF8;
                Console.OutputEncoding = System.Text.Encoding.UTF8;

                string exePath = Assembly.GetExecutingAssembly().Location;
                string? exeDirectory = Path.GetDirectoryName(exePath);
                if (string.IsNullOrEmpty(exeDirectory))
                {
                    throw new Exception("Cannot determine executable directory");
                }

                string basePath = Path.Combine(exeDirectory, "Data");

                // Ініціалізація сервісів
                IDictionaryLoader dictionaryLoader = new DictionaryLoaderService();
                IConsoleUIService uiService = new ConsoleUIService();
                var config = dictionaryLoader.LoadLanguageConfig(basePath);
                
                while (true)
                {
                    ShowMainMenu();
                    
                    Console.Write($"\n{THINKING_EMOJI} Select an action: ");
                    string? input = Console.ReadLine()?.Trim().ToLower();
                    
                    if (input == "exit" || input == "3")
                    {
                        Console.WriteLine($"\n{EXIT_EMOJI} Goodbye!");
                        break;
                    }
                    
                    if (input == "start" || input == "1")
                    {
                        bool stayInLanguageSelection = true;
                        
                        while (stayInLanguageSelection)
                        {
                            try
                            {
                                uiService.ClearConsole();
                                
                                var selectedLanguage = uiService.SelectLanguage(config);
                                
                                uiService.ClearConsole();
                                
                                ITranslationService translator = new TranslationService(selectedLanguage, basePath, dictionaryLoader);
                                
                                bool continueApp = uiService.RunTranslationLoop(translator);
                                
                                if (!continueApp)
                                {
                                    return;
                                }
                                
                                stayInLanguageSelection = false;
                            }
                            catch (OperationCanceledException)
                            {
                                stayInLanguageSelection = false;
                            }
                        }
                        
                        uiService.ClearConsole();
                    }
                    else if (input == "help" || input == "2")
                    {
                        uiService.ClearConsole();
                        ShowHelp();
                        
                        bool validChoice = false;
                        while (!validChoice)
                        {
                            Console.Write($"\n{THINKING_EMOJI} Select an action (1-2): ");
                            string? choice = Console.ReadLine()?.Trim();
                            
                            if (choice == "2" || choice?.ToLower() == "exit")
                            {
                                Console.WriteLine($"\n{EXIT_EMOJI} Goodbye!");
                                return;
                            }
                            else if (choice == "1" || choice?.ToLower() == "menu" || choice?.ToLower() == "back")
                            {
                                validChoice = true; 
                            }
                            else
                            {
                                Console.WriteLine($"{ERROR_EMOJI} Invalid choice. Please try again.");
                            }
                        }
                        
                        uiService.ClearConsole();
                    }
                    else
                    {
                        Console.WriteLine($"{ERROR_EMOJI} Invalid choice. Please try again.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ERROR_EMOJI} Error: {ex.Message}");
                Console.WriteLine($"Executable path: {Assembly.GetExecutingAssembly().Location}");

                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
            }
        }
        
        private static void ShowMainMenu()
        {
            Console.WriteLine("\n=== Multi-Language Translator ===");
            Console.WriteLine($"{TRANSLATE_EMOJI} Welcome to the console translator!");
            Console.WriteLine("\nSelect an action:");
            Console.WriteLine("1. Start translation");
            Console.WriteLine("2. Help");
            Console.WriteLine("3. Exit");
            Console.WriteLine("\nEnter item number or command (start, help, exit)");
        }
        
        private static void ShowHelp()
        {
            Console.WriteLine("\n=== Help ===");
            Console.WriteLine("This program allows you to translate text between language pairs.");
            Console.WriteLine("\nInstructions:");
            Console.WriteLine("1. Select a language pair from the list");
            Console.WriteLine("2. Enter text to translate");
            Console.WriteLine("3. Get translation result");
            Console.WriteLine("\nCommands in translation mode:");
            Console.WriteLine("'exit' - exit the program");
            Console.WriteLine("'menu' or 'back' - return to the main menu");
            
            Console.WriteLine("\nSelect an action:");
            Console.WriteLine("1. Return to main menu");
            Console.WriteLine("2. Exit program");
        }
    }
}

