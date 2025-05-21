# Tests for the Translator Project

## Description

This project contains unit tests for the "Translator" application. Tests cover the main components of the application:

- Translation service `TranslationService`
- Dictionary loading service `DictionaryLoaderService`
- Console UI service `ConsoleUIService`
- Text processing `TextProcessor`
- Main menu management `MainMenuManager`
- Integration tests for translation functionality

## Running the tests

### From Visual Studio (Recommended)

1. Open the solution `Translator.sln`
2. Right-click on the solution in Solution Explorer
3. Select "Run All Tests"

### From command line

Run the following command from the solution root directory:

```
dotnet test
```

## Test Structure

- `MainMenuManagerTests.cs` - tests for the main menu functionality
- `TranslationIntegrationTests.cs` - integration tests for translation features
- `DictionaryLoaderServiceTests.cs` - tests for dictionary loading
- `ConsoleUIServiceTests.cs` - tests for the user interface
- `TextProcessorTests.cs` - tests for text processing functionality

## Test Data

The `TestData` directory contains sample dictionaries and test files used by the tests.

## Tools Used

- xUnit - framework for writing unit tests
- Moq - library for creating mock objects 