using Moq;
using Translator.Models;
using Translator.Services;
using Translator.Services.Interfaces;
using Xunit;

namespace Translator.Tests.Services
{
    public class ConsoleUIServiceTests
    {
        private readonly ConsoleUIService _service;

        public ConsoleUIServiceTests()
        {
            _service = new ConsoleUIService();
        }

        [Fact]
        public void ClearConsole_DoesNotThrow()
        {
            var exception = Record.Exception(() => _service.ClearConsole());
            Assert.Null(exception);
        }

        [Fact]
        public void SelectLanguage_SingleLanguage_ReturnsThatLanguage()
        {
            var language = new Language
            {
                Code = "uk-en",
                Name = "Ukrainian-English",
                SourceLanguage = "Ukrainian",
                TargetLanguage = "English"
            };

            var config = new LanguageConfig
            {
                AvailableLanguages = new List<Language> { language }
            };
            
            var result = _service.SelectLanguage(config);
            
            Assert.Equal(language, result);
        }

        [Fact]
        public void SelectLanguage_NoLanguages_ThrowsException()
        {
            var config = new LanguageConfig
            {
                AvailableLanguages = new List<Language>()
            };
            
            Assert.Throws<Exception>(() => _service.SelectLanguage(config));
        }

        [Fact]
        public void RunTranslationLoop_Exit_ReturnsFalse()
        {
            var mockTranslationService = new Mock<ITranslationService>();
            var language = new Language
            {
                Code = "uk-en",
                SourceLanguage = "Ukrainian",
                TargetLanguage = "English"
            };
            mockTranslationService.Setup(ts => ts.CurrentLanguage).Returns(language);
            
            var input = new StringReader("exit");
            Console.SetIn(input);
            
            bool result;
            using (var _ = new StringWriter()) 
            {
                Console.SetOut(_);
                result = _service.RunTranslationLoop(mockTranslationService.Object);
            }
            
            Assert.False(result);
        }

        [Fact]
        public void RunTranslationLoop_Menu_ReturnsTrue()
        {
            var mockTranslationService = new Mock<ITranslationService>();
            var language = new Language
            {
                Code = "uk-en",
                SourceLanguage = "Ukrainian",
                TargetLanguage = "English"
            };
            mockTranslationService.Setup(ts => ts.CurrentLanguage).Returns(language);
            
            var input = new StringReader("menu");
            Console.SetIn(input);


            bool result;
            using (var _ = new StringWriter()) 
            {
                Console.SetOut(_);
                result = _service.RunTranslationLoop(mockTranslationService.Object);
            }
            
            Assert.True(result);
        }
        
        [Fact]
        public void RunTranslationLoop_Back_ReturnsTrue()
        {
            var mockTranslationService = new Mock<ITranslationService>();
            var language = new Language
            {
                Code = "uk-en",
                SourceLanguage = "Ukrainian",
                TargetLanguage = "English"
            };
            mockTranslationService.Setup(ts => ts.CurrentLanguage).Returns(language);
            
            var input = new StringReader("back");
            Console.SetIn(input);
            
            bool result;
            using (var _ = new StringWriter()) 
            {
                Console.SetOut(_);
                result = _service.RunTranslationLoop(mockTranslationService.Object);
            }
            
            Assert.True(result);
        }
    }
} 