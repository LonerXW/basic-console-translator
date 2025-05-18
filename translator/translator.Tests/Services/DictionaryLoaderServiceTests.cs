using System.Text;
using System.Text.Json;
using Translator.Models;
using Translator.Services;
using Xunit;

namespace Translator.Tests.Services
{
    public class DictionaryLoaderServiceTests : IDisposable
    {
        private readonly string _tempDirectory;
        
        public DictionaryLoaderServiceTests()
        {
            _tempDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(_tempDirectory);
            Directory.CreateDirectory(Path.Combine(_tempDirectory, "Languages"));
        }
        
        [Fact]
        public void LoadLanguageConfig_FileExists_ReturnsConfig()
        {
            var config = new LanguageConfig
            {
                AvailableLanguages = new List<Language>
                {
                    new Language
                    {
                        Code = "uk-en",
                        Name = "Ukrainian-English",
                        SourceLanguage = "Ukrainian",
                        TargetLanguage = "English",
                        SourceFlag = "🇺🇦",
                        TargetFlag = "🇬🇧"
                    }
                }
            };

            string configPath = Path.Combine(_tempDirectory, "Languages", "languages.json");
            string configJson = JsonSerializer.Serialize(config);
            File.WriteAllText(configPath, configJson, Encoding.UTF8);
            
            var service = new DictionaryLoaderService();
            
            var result = service.LoadLanguageConfig(_tempDirectory);
            
            Assert.NotNull(result);
            Assert.NotEmpty(result.AvailableLanguages);
            Assert.Equal(config.AvailableLanguages[0].Code, result.AvailableLanguages[0].Code);
        }
        
        [Fact]
        public void LoadLanguageConfig_FileDoesNotExist_ReturnsEmptyConfig()
        {
            var service = new DictionaryLoaderService();
            string nonExistingPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            
            var result = service.LoadLanguageConfig(nonExistingPath);
            
            Assert.NotNull(result);
            Assert.Empty(result.AvailableLanguages);
        }
        
        [Fact]
        public void LoadDictionary_ValidPaths_LoadsDictionary()
        {
            var language = new Language { Code = "uk-en" };
            
            string langDir = Path.Combine(_tempDirectory, "Languages", language.Code);
            Directory.CreateDirectory(langDir);
            
            var wordsDict = new Dictionary<string, string>
            {
                { "привіт", "hello" },
                { "світ", "world" }
            };
            
            var phrasesDict = new Dictionary<string, string>
            {
                { "як справи", "how are you" },
                { "добрий день", "good day" }
            };
            
            string wordsPath = Path.Combine(langDir, "words.json");
            string phrasesPath = Path.Combine(langDir, "phrases.json");
            
            File.WriteAllText(wordsPath, JsonSerializer.Serialize(wordsDict), Encoding.UTF8);
            File.WriteAllText(phrasesPath, JsonSerializer.Serialize(phrasesDict), Encoding.UTF8);
            
            var service = new DictionaryLoaderService();
            
            var result = service.LoadDictionary(language, _tempDirectory);
            
            Assert.NotNull(result);
            Assert.Equal(4, result.Count);
            Assert.Equal("hello", result["привіт"]);
            Assert.Equal("world", result["світ"]);
            Assert.Equal("how are you", result["як справи"]);
            Assert.Equal("good day", result["добрий день"]);
        }
        
        [Fact]
        public void LoadDictionary_DirectoryNotExists_ThrowsException()
        {
            var language = new Language { Code = "uk-en" };
            var service = new DictionaryLoaderService();
            
            Assert.Throws<Exception>(() => service.LoadDictionary(language, _tempDirectory));
        }
        
        [Fact]
        public void LoadDictionary_InvalidLanguageCode_ThrowsArgumentException()
        {
            var language = new Language { Code = "invalid" };
            var service = new DictionaryLoaderService();
            
            Assert.Throws<ArgumentException>(() => service.LoadDictionary(language, _tempDirectory));
        }
        
        [Fact]
        public void Dispose_DeletesTempDirectory()
        {
            Dispose();
            
            Assert.False(Directory.Exists(_tempDirectory));
        }
        
        public void Dispose()
        {
            try
            {
                if (Directory.Exists(_tempDirectory))
                {
                    Directory.Delete(_tempDirectory, true);
                }
            }
            catch
            {
            }
        }
    }
} 