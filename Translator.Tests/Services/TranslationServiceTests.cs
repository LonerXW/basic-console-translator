using Moq;
using Translator.Models;
using Translator.Services;
using Translator.Services.Interfaces;
using Xunit;
using Assert = Xunit.Assert;

namespace Translator.Tests.Services
{
    public class TranslationServiceTests
    {
        private readonly Mock<IDictionaryLoader> _mockDictionaryLoader;
        private readonly Language _testLanguage;
        private readonly string _basePath;

        public TranslationServiceTests()
        {
            _mockDictionaryLoader = new Mock<IDictionaryLoader>();
            _testLanguage = new Language
            {
                Code = "uk-en",
                Name = "Ukrainian-English",
                SourceLanguage = "Ukrainian",
                TargetLanguage = "English",
                SourceFlag = "🇺🇦",
                TargetFlag = "🇬🇧"
            };
            _basePath = "dummy/path";
        }

        [Fact]
        public void Translate_EmptyInput_ThrowsArgumentException()
        {
            var dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            _mockDictionaryLoader
                .Setup(dl => dl.LoadDictionary(_testLanguage, _basePath))
                .Returns(dictionary);

            var service = new TranslationService(_testLanguage, _basePath, _mockDictionaryLoader.Object);

            Assert.Throws<ArgumentException>(() => service.Translate(""));
            Assert.Throws<ArgumentException>(() => service.Translate("   "));
            Assert.Throws<ArgumentException>(() => service.Translate(null!));
        }

        [Fact]
        public void Translate_ExactMatch_ReturnsTranslation()
        {
            var dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "привіт", "hello" },
                { "світ", "world" }
            };
            _mockDictionaryLoader
                .Setup(dl => dl.LoadDictionary(_testLanguage, _basePath))
                .Returns(dictionary);

            var service = new TranslationService(_testLanguage, _basePath, _mockDictionaryLoader.Object);

            string result = service.Translate("привіт");

            Assert.Equal("hello", result);
        }

        [Fact]
        public void Translate_CaseInsensitive_ReturnsTranslation()
        {
            var dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "привіт", "hello" }
            };
            _mockDictionaryLoader
                .Setup(dl => dl.LoadDictionary(_testLanguage, _basePath))
                .Returns(dictionary);

            var service = new TranslationService(_testLanguage, _basePath, _mockDictionaryLoader.Object);

            string result = service.Translate("ПрИвІт");

            Assert.Equal("hello", result);
        }

        [Fact]
        public void Translate_WordByWord_ReturnsTranslation()
        {
            var dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "привіт", "hello" },
                { "світ", "world" }
            };
            _mockDictionaryLoader
                .Setup(dl => dl.LoadDictionary(_testLanguage, _basePath))
                .Returns(dictionary);

            var service = new TranslationService(_testLanguage, _basePath, _mockDictionaryLoader.Object);

            string result = service.Translate("привіт світ");

            Assert.Equal("Hello world", result);
        }

        [Fact]
        public void Translate_WithPunctuation_PreservesPunctuation()
        {
            var dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "привіт", "hello" }
            };
            _mockDictionaryLoader
                .Setup(dl => dl.LoadDictionary(_testLanguage, _basePath))
                .Returns(dictionary);

            var service = new TranslationService(_testLanguage, _basePath, _mockDictionaryLoader.Object);

            string result = service.Translate("привіт!");

            Assert.Equal("Hello!", result);
        }

        [Fact]
        public void Translate_UnknownWords_KeepsOriginal()
        {
            var dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "привіт", "hello" }
            };
            _mockDictionaryLoader
                .Setup(dl => dl.LoadDictionary(_testLanguage, _basePath))
                .Returns(dictionary);

            var service = new TranslationService(_testLanguage, _basePath, _mockDictionaryLoader.Object);

            string result = service.Translate("привіт невідоме");

            Assert.Equal("Hello невідоме", result);
        }

        [Fact]
        public void CurrentLanguage_ReturnsCorrectLanguage()
        {
            var dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            _mockDictionaryLoader
                .Setup(dl => dl.LoadDictionary(_testLanguage, _basePath))
                .Returns(dictionary);

            var service = new TranslationService(_testLanguage, _basePath, _mockDictionaryLoader.Object);

            Assert.Equal(_testLanguage, service.CurrentLanguage);
        }
    }
} 