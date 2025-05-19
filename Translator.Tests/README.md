# Tests for the Translator Project

## Description
This project contains unit tests for the "Translator" application. Tests cover the main components of the application:
- Translation service `TranslationService`
- Dictionary loading service `DictionaryLoaderService`
- Console UI service `ConsoleUIService`
- Data models `Language` and `LanguageConfig`

## Running the tests

### From command line
Run the following command from the solution root directory:

```
dotnet test Translator.Tests
```

### From Visual Studio
1. Open the solution `Translator.sln`
2. Select menu "Test" -> "Run All Tests"

## Test Structure

- `Services/TranslationServiceTests.cs` - tests for the translation service
- `Services/DictionaryLoaderServiceTests.cs` - tests for dictionary loading
- `Services/ConsoleUIServiceTests.cs` - tests for the user interface
- `Models/LanguageTests.cs` - tests for data models

## Tools Used
- xUnit - framework for writing unit tests
- Moq - library for creating mock objects 