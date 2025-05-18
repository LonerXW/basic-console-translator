using Translator.Models;

namespace Translator.Services.Interfaces
{
    public interface ITranslationService
    {
        string Translate(string input);
        
        Language CurrentLanguage { get; }
    }
} 