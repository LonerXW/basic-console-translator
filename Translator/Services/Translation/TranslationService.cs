using Translator.Core.Interfaces;
using Translator.Core.Models;

namespace Translator.Services.Translation
{
    /// <summary>
    /// Provides translation functionality using a dictionary and language configuration.
    /// </summary>
    public class TranslationService : ITranslationService
    {
        private readonly Dictionary<string, string> _dictionary;
        private readonly Language _currentLanguage;
        private readonly ITextProcessor _textProcessor;

        /// <summary>
        /// Initializes a new instance of the TranslationService.
        /// </summary>
        /// <param name="dictionary">The dictionary containing words and phrases for translation.</param>
        /// <param name="language">The language configuration for the translation.</param>
        public TranslationService(Dictionary<string, string> dictionary, Language language)
        {
            _dictionary = dictionary;
            _currentLanguage = language;
            _textProcessor = new TextProcessor();
        }

        /// <summary>
        /// Gets the current language configuration used for translation.
        /// </summary>
        public Language CurrentLanguage => _currentLanguage;

        /// <summary>
        /// Translates the input text using the loaded dictionary.
        /// The method first replaces phrases, then translates individual words, and finally capitalizes the first letter of the result.
        /// </summary>
        /// <param name="input">The input text to translate.</param>
        /// <returns>The translated text.</returns>
        public string Translate(string input)
        {
            input = _textProcessor.NormalizeInput(input);

            // 1. First, search for phrases (keys with spaces)
            foreach (var phrase in _dictionary.Keys.Where(k => k.Contains(' ')).OrderByDescending(k => k.Length))
            {
                if (input.Contains(phrase, StringComparison.OrdinalIgnoreCase))
                {
                    input = input.Replace(phrase, _dictionary[phrase], StringComparison.OrdinalIgnoreCase);
                }
            }

            // 2. Then translate individual words
            string[] words = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var translatedWords = words.Select(word =>
            {
                var (wordWithoutPunctuation, punctuation) = _textProcessor.SplitWordFromPunctuation(word);
                if (_dictionary.TryGetValue(wordWithoutPunctuation.ToLower(), out string? translation))
                {
                    return translation + punctuation;
                }
                return word + punctuation;
            });

            // Capitalize only the first letter of the entire sentence
            var result = string.Join(" ", translatedWords).Trim();
            if (result.Length > 0)
            {
                result = char.ToUpper(result[0]) + result.Substring(1);
            }

            return result;
        }
    }
} 