using Translator.Models;
using Xunit;
using Assert = Xunit.Assert;

namespace Translator.Tests.Models
{
    public class LanguageTests
    {
        [Fact]
        public void DisplayName_ShouldReturnCorrectFormat()
        {
            // Arrange
            var language = new Language
            {
                SourceLanguage = "Ukrainian",
                TargetLanguage = "English",
                SourceFlag = "🇺🇦",
                TargetFlag = "🇬🇧"
            };

            // Act
            string displayName = language.DisplayName;

            // Assert
            Assert.Equal("🇺🇦 Ukrainian - 🇬🇧 English", displayName);
        }

        [Fact]
        public void Language_DefaultProperties_ShouldBeInitialized()
        {
            // Arrange & Act
            var language = new Language();

            // Assert
            Assert.Equal(string.Empty, language.Code);
            Assert.Equal(string.Empty, language.Name);
            Assert.Equal(string.Empty, language.SourceLanguage);
            Assert.Equal(string.Empty, language.TargetLanguage);
            Assert.Equal(string.Empty, language.SourceScript);
            Assert.Equal(string.Empty, language.TargetScript);
            Assert.Equal(string.Empty, language.SourceFlag);
            Assert.Equal(string.Empty, language.TargetFlag);
        }

        [Fact]
        public void LanguageConfig_DefaultAvailableLanguages_ShouldBeEmpty()
        {
            // Arrange & Act
            var config = new LanguageConfig();

            // Assert
            Assert.NotNull(config.AvailableLanguages);
            Assert.Empty(config.AvailableLanguages);
        }
    }
}