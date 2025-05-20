using Translator.Services.Translation;
using Xunit;
using Assert = Xunit.Assert;

namespace Translator.Tests;

public class TextProcessorTests
{
    private readonly TextProcessor _processor = new();

    [Fact]
    public void NormalizeInput_TrimsAndThrowsOnEmpty()
    {
        Assert.Equal("hello", _processor.NormalizeInput("  hello  "));
        Assert.Throws<ArgumentException>(() => _processor.NormalizeInput("   "));
    }

    [Fact]
    public void CapitalizeFirstLetter_Works()
    {
        Assert.Equal("Hello", _processor.CapitalizeFirstLetter("hello"));
        Assert.Equal("Hello", _processor.CapitalizeFirstLetter("HELLO"));
        Assert.Equal("", _processor.CapitalizeFirstLetter(""));
    }

    [Fact]
    public void SplitWordFromPunctuation_Works()
    {
        var (word, punct) = _processor.SplitWordFromPunctuation("hello!");
        Assert.Equal("hello", word);
        Assert.Equal("!", punct);

        (word, punct) = _processor.SplitWordFromPunctuation("world");
        Assert.Equal("world", word);
        Assert.Equal("", punct);
    }
}