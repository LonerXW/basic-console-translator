using Translator.Core.Interfaces;

namespace Translator.Services.Translation
{
    /// <summary>
    /// Provides text processing utilities for normalization, capitalization, and punctuation handling.
    /// </summary>
    public class TextProcessor : ITextProcessor
    {
        /// <summary>
        /// Normalizes the input string by trimming whitespace and validating it is not empty.
        /// </summary>
        /// <param name="input">The input string to normalize.</param>
        /// <returns>The trimmed input string.</returns>
        /// <exception cref="ArgumentException">Thrown if the input is empty or whitespace.</exception>
        public string NormalizeInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException("Input cannot be empty or whitespace", nameof(input));
            }

            return input.Trim();
        }

        /// <summary>
        /// Capitalizes the first letter of the input string and makes the rest lowercase.
        /// </summary>
        /// <param name="input">The input string to capitalize.</param>
        /// <returns>The capitalized string, or the original string if it is empty.</returns>
        public string CapitalizeFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            return char.ToUpper(input[0]) + input.Substring(1).ToLower();
        }

        /// <summary>
        /// Splits a word from its trailing punctuation.
        /// </summary>
        /// <param name="input">The input string to split.</param>
        /// <returns>A tuple containing the word and the punctuation.</returns>
        public (string word, string punctuation) SplitWordFromPunctuation(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return (string.Empty, string.Empty);
            }

            int lastLetterIndex = input.Length - 1;
            while (lastLetterIndex >= 0 && !char.IsLetterOrDigit(input[lastLetterIndex]))
            {
                lastLetterIndex--;
            }

            if (lastLetterIndex < 0)
            {
                return (string.Empty, input);
            }

            string word = input.Substring(0, lastLetterIndex + 1);
            string punctuation = input.Substring(lastLetterIndex + 1);

            return (word, punctuation);
        }
    }
} 