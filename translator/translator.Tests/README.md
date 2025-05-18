# Тести для проекту Translator

## Опис
Цей проект містить юніт-тести для додатку "Translator". Тести охоплюють основні компоненти додатку:
- Сервіс перекладу `TranslationService`
- Сервіс завантаження словників `DictionaryLoaderService`
- Сервіс консольного інтерфейсу `ConsoleUIService`
- Моделі даних `Language` та `LanguageConfig`

## Запуск тестів

### З командного рядка
Запустіть наступну команду з кореневої директорії рішення:

```
dotnet test translator.Tests
```

### З Visual Studio
1. Відкрийте рішення `translator.sln`
2. Виберіть меню "Test" -> "Run All Tests"

## Структура тестів

- `Services/TranslationServiceTests.cs` - тести для сервісу перекладу
- `Services/DictionaryLoaderServiceTests.cs` - тести для завантаження словників
- `Services/ConsoleUIServiceTests.cs` - тести для інтерфейсу користувача
- `Models/LanguageTests.cs` - тести для моделей даних

## Використані інструменти
- xUnit - фреймворк для написання юніт-тестів
- Moq - бібліотека для створення мок-об'єктів 