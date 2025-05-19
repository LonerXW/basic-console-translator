using Translator.Application;
using Translator.Services.UI;
using Translator.Infrastructure.Constants;
using System.Reflection;

namespace Translator
{
    /// <summary>
    /// The main entry point class for the translation application.
    /// Handles the initialization and main program loop.
    /// </summary>
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// Initializes the application and runs the main program loop.
        /// </summary>
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

                var initializer = new ApplicationInitializer();
                var menuManager = new MainMenuManager(
                    initializer.UiService,
                    initializer.DictionaryLoader,
                    initializer.BasePath,
                    initializer.Config
                );

                while (true)
                {
                    MenuManager.ShowMainMenu();
                    MenuManager.ShowActionPrompt();
                    string? input = Console.ReadLine()?.Trim().ToLower();
                    if (!menuManager.HandleUserChoice(input))
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MenuManager.ShowError(ex.Message);
                MenuManager.ShowExecutablePath(Assembly.GetExecutingAssembly().Location);
                MenuManager.ShowPressAnyKeyToExit();
                Console.ReadKey();
            }
        }
    }
}

