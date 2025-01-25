/*using System.Collections.Immutable;
using FluentAssertions;
using TagCloud.ImageGeneration;
using TagCloud.Parsing;
using TagCloud.ReadingFiles;

namespace TagCloud.Tests;

[TestFixture]
public class LiteraryTextParserTests
{
    private readonly LiteraryTextParser _literaryTextParser = new();
    private readonly ImmutableArray<string> _testLines;
    private readonly Dictionary<string, int> _frequencyDictionary = new()
    {
        { "привет", 5 },
        { "морозный", 7 },
        { "быстрый", 3 },
        { "я", 20 },
        { "человек", 2},
        { "отчаянно", 8},
    };
    private readonly HashSet<char> russianAlphabet = [];

    public LiteraryTextParserTests()
    {
        for (var symbol = 'а'; symbol <= 'я'; symbol++)
            russianAlphabet.Add(symbol);
        russianAlphabet.Add('ё');
        
        var lines = TxtReaderTests.CreateArrayOfWords(_frequencyDictionary);
        var random = new Random();
        random.Shuffle(lines);
        _testLines = [..lines];
    }

    [TestCase(ContentStructure.Literary)]
    [TestCase(ContentStructure.ListOfWords)]
    public void PerformPreprocessing_Text_AllCharactersMustBeInLowercase(ContentStructure typeOfContent)
    {
        var lines = new[] { "привет", "ПрИвЕт", "Привет", "ПРИВЕТ" };

        var result = _literaryTextParser.Parse(
            () => lines,
            ContentStructure.Literary);

        CheckCharactersOfWords(result, symbol => char.IsLower(symbol) || symbol == '-');
    }

    [TestCase(ContentStructure.Literary)]
    public void PerformPreprocessing_Text_OnlyRussianLettersShouldRemainInWords(ContentStructure typeOfContent)
    {
        var lines = new[] { "python?", "java!", "C#", "языки-", "программирования", "пriveт", "из-за" };
        
        var result = _literaryTextParser.Parse(
            () => lines,
            typeOfContent);
        
        CheckCharactersOfWords(result, symbol => russianAlphabet.Contains(symbol) || symbol == '-');
    }

    private void CheckCharactersOfWords(IEnumerable<WordInfo> words, Func<char, bool> check)
    {
        foreach (var wordInfo in words)
            wordInfo.Word.All(check).Should().BeTrue();
    }

    [TestCase(ContentStructure.Literary)]
    [TestCase(ContentStructure.ListOfWords)]
    public void PerformPreprocessing_Text_CorrectWordCount(ContentStructure typeOfContent)
    {
        var result = _literaryTextParser.Parse(
            () => _testLines,
            typeOfContent);
        
        result.All(wordInfo => _frequencyDictionary[wordInfo.Word] == wordInfo.NumberInText).Should().BeTrue();
    }
}*/