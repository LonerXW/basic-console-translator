namespace Translator.Core.Models;

/// <summary>
///     Represents the configuration containing a list of available language pairs for translation.
/// </summary>
public class LanguageConfig
{
    public List<Language> AvailableLanguages { get; set; } = new();
}