using Translator.Core.Models;
using Translator.Infrastructure.FileSystem;
using Translator.Services.Translation;
using Xunit;
using Assert = Xunit.Assert;

namespace Translator.Tests;

public class TranslationIntegrationTests
{
    private readonly TranslationService _service;

    public TranslationIntegrationTests()
    {
        var basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData");
        var language = new Language { Code = "en-uk", SourceLanguage = "English", TargetLanguage = "Ukrainian" };
        var loader = new DictionaryLoaderService();
        var dictionary = loader.LoadDictionary(language, basePath);
        _service = new TranslationService(dictionary, language);
    }

    [Fact]
    public void Translate_SimpleWord()
    {
        Assert.Equal("Привіт", _service.Translate("hello"));
    }

    [Fact]
    public void Translate_Phrase()
    {
        Assert.Equal("Добрий ранок", _service.Translate("good morning"));
    }

    [Fact]
    public void Translate_UnknownWord()
    {
        Assert.Equal("Unknownword", _service.Translate("unknownword"));
    }

    [Fact]
    public void Translate_TextWithPunctuation()
    {
        Assert.Equal("Привіт, світ!", _service.Translate("hello, world!"));
    }

    [Fact]
    public void Translate_TextWithCapitalization()
    {
        Assert.Equal("Привіт світ", _service.Translate("Hello world"));
    }
}