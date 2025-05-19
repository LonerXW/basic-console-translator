using Translator.Core.Models;

namespace Translator.Core.Interfaces
{
    /// <summary>
    /// Defines the interface for console-based user interface services.
    /// Provides methods for user interaction and information display.
    /// </summary>
    public interface IConsoleUIService
    {
        void ClearScreen();
        
        Language SelectLanguage(LanguageConfig config);
        
        bool RunTranslationLoop(ITranslationService translator);
    }
} 