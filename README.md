# Multi-Language Translator

A console-based translator application that works with multiple language pairs using JSON dictionaries.

## Features

- Translation between various language pairs (English-Ukrainian, Polish-English, etc.)
- Easy-to-navigate menu system
- Handles word and phrase translations
- Preserves capitalization and punctuation
- Extensible dictionary system

## Project Structure

- **Services/** - Core services implementation
  - **Interfaces/** - Service interfaces
  - **ConsoleUIService.cs** - Console user interface implementation
  - **TranslationService.cs** - Text translation logic
  - **DictionaryLoaderService.cs** - Loading dictionaries from JSON files
  
- **Models/** - Data models
  - **Language.cs** - Language pair and configuration models
  
- **Data/Languages/** - Contains language dictionaries
  - Each language pair has a dedicated folder (e.g., `en-ua`, `pl-en`)
  - Each folder contains `words.json` and `phrases.json` files

- **translator.Tests/** - Unit tests for all components

## Documentation

For comprehensive technical documentation, please refer to the `refman.pdf` file. This documentation provides detailed information about:

- Complete class reference with all available methods and properties
- Detailed method descriptions and usage examples
- Code structure and architecture overview
- Working code examples for common use cases
- Service interfaces and their implementations

The documentation is particularly useful for developers who want to:
- Understand the internal workings of the translator
- Extend the functionality with new features
- Integrate the translator into other applications
- Debug and troubleshoot issues

## Getting Started

### Prerequisites

- .NET 6.0 or later

### Running the Application

You can run the application in two ways:

1. Using Visual Studio:
   - Open the solution in Visual Studio
   - Set the `Translator` project as the startup project
   - Click the "Start" button or press F5

2. Using Command Line:
   - Navigate to the `Translator` directory
   - Run the following command:
   ```
   dotnet run
   ```

### Navigation

The application has a three-level menu system:
1. **Main Menu** - Select to start translation, get help, or exit
2. **Language Selection** - Choose a language pair or return to the main menu
3. **Translation Mode** - Enter text to translate, navigate back to previous menus, or exit

### Extending Dictionaries

To add new words or phrases:
1. Find the appropriate language pair directory in `Data/Languages/`
2. Edit the `words.json` or `phrases.json` files
3. Follow the existing JSON format

### Adding New Language Support

To add support for a new language pair (e.g., English-French):

1. Create a new directory in `Data/Languages/` with the language pair code (e.g., `en-fr` for English to French)
   ```
   Data/Languages/en-fr/
   ```

2. Create two JSON files in the new directory:
   - `words.json` - for individual word translations
   - `phrases.json` - for phrase translations

3. Format for `words.json`:
   ```json
   {
     "translations": {
       "source_word": "translated_word",
       "hello": "bonjour",
       "goodbye": "au revoir"
     }
   }
   ```

4. Format for `phrases.json`:
   ```json
   {
     "translations": {
       "source_phrase": "translated_phrase",
       "how are you": "comment allez-vous",
       "good morning": "bonjour"
     }
   }
   ```

5. Update the language configuration in `Data/languages.json` to include the new language pair:
   ```json
   {
     "languagePairs": [
       {
         "code": "en-fr",
         "sourceLanguage": "English",
         "targetLanguage": "French",
         "sourceFlag": "🇬🇧",
         "targetFlag": "🇫🇷"
       }
     ]
   }
   ```

   Configuration fields:
   - `code`: Language pair code (e.g., "en-fr")
   - `sourceLanguage`: Name of the source language
   - `targetLanguage`: Name of the target language
   - `sourceFlag`: Emoji flag of the source language country
   - `targetFlag`: Emoji flag of the target language country

   Purpose of flags:
   - Flags (`sourceFlag` and `targetFlag`):
     - Provide visual identification of languages in the user interface
     - Make language selection more intuitive
     - Help users quickly identify language pairs
     - Improve the overall user experience

6. Restart the application to see the new language pair in the selection menu

Note: Make sure to use proper UTF-8 encoding when creating the dictionary files to support special characters and diacritical marks.

## Running Tests

You can run the tests in two ways:

1. Using Visual Studio:
   - Open the solution in Visual Studio
   - Right-click on the solution in Solution Explorer
   - Select "Run All Tests"

2. Using Command Line:
   - Navigate to the solution directory
   - Run the following command:
   ```
   dotnet test
   ```

See the [test README](translator.Tests/README.md) for more details. 