namespace Translator.Models
{
    /// Модель даних для мовної пари
    public class Language
    {
        public string Code { get; set; } = "";
        

        public string Name { get; set; } = "";
        

        public string SourceLanguage { get; set; } = "";
        

        public string TargetLanguage { get; set; } = "";
        

        public string SourceScript { get; set; } = "";
        

        public string TargetScript { get; set; } = "";
        

        public string SourceFlag { get; set; } = "";
        

        public string TargetFlag { get; set; } = "";
        

        public string DisplayName => $"{SourceFlag} {SourceLanguage} - {TargetFlag} {TargetLanguage}";
    }


    public class LanguageConfig
    {
        public List<Language> AvailableLanguages { get; set; } = new();
    }
} 