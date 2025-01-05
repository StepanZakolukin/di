using FluentAssertions;
using TagCloud.TextProcessing;
using TagCloudGUI.Controls;

namespace TagCloud.Tests;

[TestFixture]
public class TextPreprocessingTests
{
    private readonly TextPreprocessing textPreprocessing = new();
    
    private readonly HashSet<char> russianAlphabet = [];

    [SetUp]
    public void Setup()
    {
        for (var symbol = 'а'; symbol <= 'я'; symbol++)
            russianAlphabet.Add(symbol);
        russianAlphabet.Add('ё');
    }

    [Test]
    public void PerformPreprocessing_Text_AllCharactersMustBeInLowercase()
    {
        var morozko = Path.Combine(Directory.GetCurrentDirectory(), "Texts", "Morozko.txt");

        var result = textPreprocessing.PerformPreprocessing(morozko, FileContentStructure.Literary);

        CheckCharactersOfWords(result, symbol => char.IsLower(symbol) || symbol == '-');
}

    [Test]
    public void PerformPreprocessing_Text_OnlyRussianLettersShouldRemainInWords()
    {
        var eugeneOnegin = Path.Combine(Directory.GetCurrentDirectory(), "Texts", "EugeneOnegin.txt");
        
        var result = textPreprocessing.PerformPreprocessing(eugeneOnegin, FileContentStructure.Literary);
        
        CheckCharactersOfWords(result, symbol => russianAlphabet.Contains(symbol) || symbol == '-');
    }

    private void CheckCharactersOfWords(IEnumerable<WordInfo> words, Func<char, bool> check)
    {
        foreach (var wordInfo in words)
            wordInfo.Word.All(check).Should().BeTrue();
    }

    [Test]
    public void PerformPreprocessing_Text_CorrectWordCount()
    {
        var pathToFile = "../../../Texts/CheckingCount.txt";
        var frequencyDictionary = new Dictionary<string, int>
        {
            { "привет", 5 },
            { "морозный", 7 },
            { "быстрый", 3 },
            { "я", 20 },
            { "человек", 2},
            { "отчаянно", 8},
        };
        var lines = CreateArrayOfWords(frequencyDictionary);
        var random = new Random();
        random.Shuffle(lines);
        File.WriteAllLines(pathToFile, lines);
        
        var result = textPreprocessing.PerformPreprocessing(pathToFile, FileContentStructure.Literary);
        
        result.All(wordInfo => frequencyDictionary[wordInfo.Word] == wordInfo.NumberInText).Should().BeTrue();
    }

    private string[] CreateArrayOfWords(Dictionary<string, int> frequencyDictionary)
    {
        var list = new List<string>();
        foreach (var pair in frequencyDictionary)
            for (var i = 0; i < pair.Value; i++)
                list.Add(pair.Key);
        
        return list.ToArray();
    }

    [Test]
    public void PerformPreprocessing_UnExistingFile_ThrowsFileNotFoundException()
    {
        var calling = () => textPreprocessing.PerformPreprocessing(
            "UnExistingFile.txt",
            FileContentStructure.Literary);
        
        calling.Should().Throw<FileNotFoundException>();
    }

    [Test]
    public void PerformPreprocessing_EmptyFile_EmptyCollectionOfWords()
    {
        var pathToFile = Path.Combine(Directory.GetCurrentDirectory(), "Texts", "EmptyFile.txt");
        var literaryStructure = textPreprocessing.PerformPreprocessing(pathToFile, FileContentStructure.Literary);
        var listOfWords = textPreprocessing.PerformPreprocessing(pathToFile, FileContentStructure.ListOfWords);
        
        listOfWords.Should().BeEmpty();
        literaryStructure.Should().BeEmpty();
    }
}