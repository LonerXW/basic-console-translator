using System.Reflection;
using System.Text;
using Translator.Application;
using Translator.Services.UI;

namespace Translator;

/// <summary>
///     The main entry point class for the translation application.
///     Handles the initialization and main program loop.
/// </summary>
internal static class Program
{
    /// <summary>
    ///     The main entry point for the application.
    ///     Initializes the application and runs the main program loop.
    /// </summary>
    public static void Main()
    {
        try
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            var exePath = Assembly.GetExecutingAssembly().Location;
            var exeDirectory = Path.GetDirectoryName(exePath);
            if (string.IsNullOrEmpty(exeDirectory)) throw new Exception("Cannot determine executable directory");

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
                var input = Console.ReadLine()?.Trim().ToLower();
                if (!menuManager.HandleUserChoice(input)) break;
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