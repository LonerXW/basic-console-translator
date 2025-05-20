using Moq;
using Translator.Core.Interfaces;
using Translator.Core.Models;
using Translator.Services.UI;
using Xunit;
using Assert = Xunit.Assert;

namespace Translator.Tests;

/// <summary>
///     Contains unit tests for the MainMenuManager class.
/// </summary>
public class MainMenuManagerTests
{
    /// <summary>
    ///     Verifies that the application exits when the user selects the exit option.
    /// </summary>
    [Fact]
    public void HandleUserChoice_Exit_ReturnsFalse()
    {
        var mockUI = new Mock<IConsoleUIService>();
        var mockLoader = new Mock<IDictionaryLoader>();
        var config = new LanguageConfig();
        var manager = new MainMenuManager(mockUI.Object, mockLoader.Object, "base", config);
        Assert.False(manager.HandleUserChoice("exit"));
        Assert.False(manager.HandleUserChoice("3"));
    }

    /// <summary>
    ///     Verifies that the application continues running and displays an error message
    ///     when the user enters an invalid choice.
    /// </summary>
    [Fact]
    public void HandleUserChoice_InvalidChoice_PrintsErrorAndReturnsTrue()
    {
        var mockUI = new Mock<IConsoleUIService>();
        var mockLoader = new Mock<IDictionaryLoader>();
        var config = new LanguageConfig();
        var manager = new MainMenuManager(mockUI.Object, mockLoader.Object, "base", config);
        Assert.True(manager.HandleUserChoice("invalid"));
    }
}