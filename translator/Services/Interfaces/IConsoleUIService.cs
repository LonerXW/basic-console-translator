using Translator.Models;

namespace Translator.Services.Interfaces
{
    public interface IConsoleUIService
    {
        Language SelectLanguage(LanguageConfig config);
        
        bool RunTranslationLoop(ITranslationService translationService);
        
        void ClearConsole();
    }
} 