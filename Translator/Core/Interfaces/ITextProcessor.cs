namespace Translator.Core.Interfaces;

/// <summary>
///     Defines the interface for text processing utilities such as normalization and punctuation handling.
/// </summary>
public interface ITextProcessor
{
    string NormalizeInput(string input);

    (string word, string punctuation) SplitWordFromPunctuation(string input);
}