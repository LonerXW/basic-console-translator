using Translator.Models;
using Translator.Services.Interfaces;

namespace Translator.Services
{
    /// Сервіс перекладу тексту між мовними парами
    public class TranslationService : ITranslationService
    {
        private readonly Dictionary<string, string> _dictionary;
        private readonly Language _language;


        public Language CurrentLanguage => _language;


        public TranslationService(Language language, string basePath, IDictionaryLoader dictionaryLoader)
        {
            _language = language;
            _dictionary = dictionaryLoader.LoadDictionary(language, basePath);
        }
        
        public string Translate(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Input text cannot be empty");
            
            string normalizedInput = input.ToLowerInvariant().Trim();
            
            if (_dictionary.TryGetValue(normalizedInput, out string? exactMatch))
            {
                if (!normalizedInput.Contains(" "))
                {
                    return exactMatch;
                }
                return CapitalizeFirstLetter(exactMatch);
            }
            
            // Переклад по словам
            string[] words = normalizedInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            List<string> resultWords = new List<string>();
            
            foreach (var word in words)
            {
                string wordOnly = word;
                string punctuation = "";
                
                int lastLetterIndex = word.Length - 1;
                while (lastLetterIndex >= 0 && !char.IsLetterOrDigit(word[lastLetterIndex]))
                {
                    lastLetterIndex--;
                }
                
                if (lastLetterIndex < word.Length - 1)
                {
                    wordOnly = word.Substring(0, lastLetterIndex + 1);
                    punctuation = word.Substring(lastLetterIndex + 1);
                }
                else if (lastLetterIndex == -1) // Слово складається тільки з пунктуації
                {
                    resultWords.Add(word);
                    continue;
                }
                
                string wordLower = wordOnly.ToLowerInvariant();
                
                if (_dictionary.TryGetValue(wordLower, out string? wordMatch))
                {
                    if (resultWords.Count == 0)
                    {
                        wordMatch = CapitalizeFirstLetter(wordMatch);
                    }
                    resultWords.Add(wordMatch + punctuation);
                }
                else
                {
                    if (resultWords.Count == 0)
                    {
                        wordOnly = CapitalizeFirstLetter(wordOnly);
                    }
                    resultWords.Add(wordOnly + punctuation);
                }
            }
            
            string result = string.Join(" ", resultWords);
            return result;
        }


        private string CapitalizeFirstLetter(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;
        
            return char.ToUpper(text[0]) + text.Substring(1);
        }
    }
}
