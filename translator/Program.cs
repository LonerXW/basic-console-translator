using System.Text.Json;
using System.Text.RegularExpressions;

namespace Kursach
{
    class Program
    {
        static void Main()
        {
            string dataPath = "";

            var phraseDict = LoadDict(Path.Combine(dataPath, ""));
            var wordDict = LoadDict(Path.Combine(dataPath, ""));

            var reversePhraseDict = ReverseDict(phraseDict);
            var reverseWordDict = ReverseDict(wordDict);

            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine($"Фраз: {phraseDict.Count}, слів: {wordDict.Count}");

            while (true)
            {
                Console.Write("\nВведи речення (або 'exit'): ");
                string input = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(input)) continue;
                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase)) break;

                bool isUkrainian = IsCyrillic(input);

                string? fullPhrase;
                if (isUkrainian)
                {
                    if (reversePhraseDict.TryGetValue(input, out fullPhrase))
                        Console.WriteLine($"🌐 Повна фраза: {fullPhrase}");
                    else
                        Console.WriteLine("🧠 " + Translate(input, reverseWordDict));
                }
                else
                {
                    if (phraseDict.TryGetValue(input, out fullPhrase))
                        Console.WriteLine($"Full phrase: {fullPhrase}");
                    else
                        Console.WriteLine("" + Translate(input, wordDict));
                }
            }

            Console.WriteLine("\n Вихід...");
        }

        static Dictionary<string, string> LoadDict(string path)
        {
            return JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(path))!;
        }

        static Dictionary<string, string> ReverseDict(Dictionary<string, string> dict)
        {
            return dict.GroupBy(kvp => kvp.Value)
                       .ToDictionary(g => g.Key, g => g.First().Key);
        }

        static bool IsCyrillic(string text)
        {
            return text.Any(c => c >= 0x0400 && c <= 0x04FF);
        }

        static string Translate(string sentence, Dictionary<string, string> dict)
        {
            var parts = sentence.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var result = new List<string>();

            foreach (var part in parts)
            {
                string punctuation = "";
                string word = part.Trim();

                var m = Regex.Match(word, @"(.+?)([.,!?…]*)$");
                if (m.Success)
                {
                    word = m.Groups[1].Value;
                    punctuation = m.Groups[2].Value;
                }

                string lower = word.ToLowerInvariant();
                if (dict.TryGetValue(lower, out string translated))
                    result.Add(translated + punctuation);
                else
                    result.Add(word + punctuation);
            }

            if (result.Count > 0)
                result[0] = Capitalize(result[0]);

            return string.Join(" ", result);
        }

        static string Capitalize(string word)
        {
            return string.IsNullOrEmpty(word) ? word :
                   char.ToUpper(word[0]) + word.Substring(1);
        }
    }
}
