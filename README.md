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

## Getting Started

### Prerequisites

- .NET 6.0 or later

### Running the Application

From the command line, navigate to the project directory and run:

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

## Running Tests

```
dotnet test translator.Tests
```

See the [test README](translator.Tests/README.md) for more details. 