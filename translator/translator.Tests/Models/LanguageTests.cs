using Translator.Models;
using Xunit;

namespace Translator.Tests.Models
{
    public class LanguageTests
    {
        [Fact]
        public void DisplayName_ReturnsCorrectFormat()
        {
            var language = new Language
            {
                SourceLanguage = "Ukrainian",
                TargetLanguage = "English",
                SourceFlag = "🇺🇦",
                TargetFlag = "🇬🇧"
            };
            
            string displayName = language.DisplayName;
            
            Assert.Equal("🇺🇦 Ukrainian - 🇬🇧 English", displayName);
        }
        
        [Fact]
        public void Language_DefaultProperties_AreInitialized()
        {
            var language = new Language();
            
            Assert.Equal("", language.Code);
            Assert.Equal("", language.Name);
            Assert.Equal("", language.SourceLanguage);
            Assert.Equal("", language.TargetLanguage);
            Assert.Equal("", language.SourceScript);
            Assert.Equal("", language.TargetScript);
            Assert.Equal("", language.SourceFlag);
            Assert.Equal("", language.TargetFlag);
        }
        
        [Fact]
        public void LanguageConfig_DefaultAvailableLanguages_IsInitialized()
        {
            var config = new LanguageConfig();
            
            Assert.NotNull(config.AvailableLanguages);
            Assert.Empty(config.AvailableLanguages);
        }
    }
} 