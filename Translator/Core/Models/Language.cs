namespace Translator.Core.Models
{
    /// <summary>
    /// Represents a language pair and associated metadata for translation.
    /// </summary>
    public class Language
    {
        public string Code { get; set; } = string.Empty;
        
        public string SourceLanguage { get; set; } = string.Empty;
        
        public string TargetLanguage { get; set; } = string.Empty;
        
        public string SourceFlag { get; set; } = string.Empty;
        
        public string TargetFlag { get; set; } = string.Empty;
    }
} 