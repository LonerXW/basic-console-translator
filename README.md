# 🌐 English-Ukrainian Translator

Simple console-based translator that supports English-Ukrainian translation in both directions.

## ✨ Features

- Automatic language detection (English/Ukrainian)
- Support for both single words and common phrases
- Case-insensitive translation
- Preserves punctuation
- Easy to extend dictionary

## 📦 Requirements

- .NET 6.0 or higher
- UTF-8 console support

## 🚀 How to Use

1. Run the application
2. Type your text in English or Ukrainian
3. Press Enter to get the translation
4. Type 'exit' to close the application

## 📚 Dictionary Structure

The translator uses two JSON dictionaries located in the `Data` folder:
- `words.json` - for single word translations
- `phrases.json` - for common phrases and expressions

## 🔧 How to Add New Words/Phrases

To add new translations, simply edit the corresponding JSON file:

### For words.json:
```json
{
    "english_word": "український_переклад"
}
```

### For phrases.json:
```json
{
    "english phrase": "український переклад фрази"
}
```

## 📝 Examples

Input: "Hello world"
Output: "Привіт світ"

Input: "Доброго ранку"
Output: "Good morning"