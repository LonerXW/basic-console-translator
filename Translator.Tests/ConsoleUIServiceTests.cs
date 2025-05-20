using Translator.Services.UI;
using Xunit;
using Assert = Xunit.Assert;

namespace Translator.Tests;

public class ConsoleUIServiceTests
{
    [Fact]
    public void DisplayMessage_WritesToConsole()
    {
        var originalOut = Console.Out;
        try
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                var service = new ConsoleUIService();
                service.DisplayMessage("test");
                Assert.Contains("test", sw.ToString());
            }
        }
        finally
        {
            Console.SetOut(originalOut);
        }
    }

    [Fact]
    public void GetUserInput_ReadsFromConsole()
    {
        var originalIn = Console.In;
        try
        {
            Console.SetIn(new StringReader("input\n"));
            var service = new ConsoleUIService();
            Assert.Equal("input", service.GetUserInput());
        }
        finally
        {
            Console.SetIn(originalIn);
        }
    }
}