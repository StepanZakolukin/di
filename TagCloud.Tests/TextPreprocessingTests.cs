using FluentAssertions;
using TagCloud.ReadingFiles;
using TagCloud.TextProcessing;
using TagCloudGUI.Controls;

namespace TagCloud.Tests;

[TestFixture]
public class TextPreprocessingTests
{
    private readonly TextPreprocessing textPreprocessing = new();
    private readonly IReaderProvider readerProvider = new ReaderPicker([new TxtReader()]);
    private readonly string pathToFileFolder = Path.Combine(Directory.GetCurrentDirectory(), "TestsFiles");
    
    private readonly HashSet<char> russianAlphabet = [];

    [SetUp]
    public void Setup()
    {
        for (var symbol = 'а'; symbol <= 'я'; symbol++)
            russianAlphabet.Add(symbol);
        russianAlphabet.Add('ё');
    }

    [TestCase(ContentStructure.Literary)]
    [TestCase(ContentStructure.ListOfWords)]
    public void PerformPreprocessing_Text_AllCharactersMustBeInLowercase(ContentStructure typeOfContent)
    {
        var morozko = Path.Combine(pathToFileFolder, "Morozko.txt");

        var result = textPreprocessing.PerformPreprocessing(
            readerProvider.GetReader(morozko),
            ContentStructure.Literary);

        CheckCharactersOfWords(result, symbol => char.IsLower(symbol) || symbol == '-');
}

    [Test]
    public void PerformPreprocessing_Text_OnlyRussianLettersShouldRemainInWords()
    {
        var eugeneOnegin = Path.Combine(pathToFileFolder, "EugeneOnegin.txt");
        
        var result = textPreprocessing.PerformPreprocessing(
            readerProvider.GetReader(eugeneOnegin),
            ContentStructure.Literary);
        
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
        var pathToFile = Path.Combine(pathToFileFolder, "CheckingCount.txt");
        var frequencyDictionary = new Dictionary<string, int>
        {
            { "привет", 5 },
            { "морозный", 7 },
            { "быстрый", 3 },
            { "я", 20 },
            { "человек", 2},
            { "отчаянно", 8},
        };
        
        var lines = TxtReaderTests.CreateArrayOfWords(frequencyDictionary);
        var random = new Random();
        random.Shuffle(lines);
        File.WriteAllLines(pathToFile, lines);
        
        var result = textPreprocessing.PerformPreprocessing(
            readerProvider.GetReader(pathToFile),
            typeOfContent);
        
        result.All(wordInfo => frequencyDictionary[wordInfo.Word] == wordInfo.NumberInText).Should().BeTrue();
    }
}