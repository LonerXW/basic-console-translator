using System.Text.Json;
using Translator.Core.Models;
using Translator.Infrastructure.Constants;
using Translator.Infrastructure.FileSystem;
using Xunit;
using Assert = Xunit.Assert;

namespace Translator.Tests;

/// <summary>
///     Tests for the dictionary loader service.
/// </summary>
public class DictionaryLoaderServiceTests : IDisposable
{
    private readonly string _tempDir;

    /// <summary>
    ///     Initializes a new instance of the tests, creating a temporary directory for test data.
    /// </summary>
    public DictionaryLoaderServiceTests()
    {
        _tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(_tempDir);
        Directory.CreateDirectory(FileStructureConstants.GetLanguageDirectoryPath(_tempDir, "en-uk"));
    }

    /// <summary>
    ///     Releases resources by deleting the temporary test data directory.
    /// </summary>
    public void Dispose()
    {
        if (Directory.Exists(_tempDir)) Directory.Delete(_tempDir, true);
    }

    /// <summary>
    ///     Verifies the correct loading of words and phrases from JSON files.
    /// </summary>
    [Fact]
    public void LoadDictionary_LoadsWordsAndPhrases()
    {
        var words = new Dictionary<string, string> { { "hello", "привіт" } };
        var phrases = new Dictionary<string, string> { { "good morning", "добрий ранок" } };
        File.WriteAllText(FileStructureConstants.GetWordsFilePath(_tempDir, "en-uk"), JsonSerializer.Serialize(words));
        File.WriteAllText(FileStructureConstants.GetPhrasesFilePath(_tempDir, "en-uk"),
            JsonSerializer.Serialize(phrases));
        var loader = new DictionaryLoaderService();
        var lang = new Language { Code = "en-uk" };
        var dict = loader.LoadDictionary(lang, _tempDir);
        Assert.Equal("привіт", dict["hello"]);
        Assert.Equal("добрий ранок", dict["good morning"]);
    }

    /// <summary>
    ///     Verifies that an exception is thrown when the language directory is missing.
    /// </summary>
    [Fact]
    public void LoadDictionary_ThrowsOnMissingDir()
    {
        var loader = new DictionaryLoaderService();
        var lang = new Language { Code = "en-uk" };
        Assert.Throws<Exception>(() => loader.LoadDictionary(lang, Path.Combine(_tempDir, "notfound")));
    }
}