namespace Translator.Core.Interfaces
{
    /// <summary>
    /// Defines the interface for translation services.
    /// </summary>
    public interface ITranslationService
    {
        string Translate(string input);
    }
} 