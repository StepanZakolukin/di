using System.Diagnostics;

namespace TagCloud.TextProcessing;

public class TextPreprocessing
{
    private readonly Dictionary<string, string> decryptionGrammems = new()
    {
        { "A", "прилагательное" },
        { "ADV", "наречие" },
        { "ADVPRO", "местоименное наречие" },
        {"ANUM", "числительное-прилагательное" },
        {"APRO", "местоимение-прилагательное" },
        { "COM", "часть композита - сложного слова" },
        { "CONJ", "союз" },
        { "INTJ", "междометие" },
        {"NUM", "числительное" },
        { "PART", "частица" },
        {"PR", "предлог" },
        { "S", "существительное" },
        { "SPRO", "местоимение-существительное" },
        { "V", "глагол" }
    };
    
    
    public IEnumerable<WordInfo> PerformPreprocessing(string pathToSourceTxtFile)
    {
        var textInfo = ParseText(pathToSourceTxtFile);
        var countingDictionary = new Dictionary<Tuple<string, string>, int>();

        for (var i = 0; i < textInfo.Length - 1; i++)
        {
            var info = textInfo[i].Split('=');
            var wordAndPartOfSpeech = Tuple.Create(info[0], info[1].Split(',')[0]);
            if (countingDictionary.ContainsKey(wordAndPartOfSpeech))
                countingDictionary[wordAndPartOfSpeech]++;
            else countingDictionary[wordAndPartOfSpeech] = 1;
        }

        foreach (var pair in countingDictionary)
        {
            yield return new WordInfo(pair.Key.Item1,
                decryptionGrammems[pair.Key.Item2],
                pair.Value);
        }
    }
    
    private string[] ParseText(string pathToSourceTxtFile)
    {
        var outputFile = "TextProcessing/Mystem/out.txt";
        var startInfo = new ProcessStartInfo
        {
            UseShellExecute = false,
            RedirectStandardInput = false,
            RedirectStandardOutput = false,
            FileName = "TextProcessing/Mystem/mystem.exe",
            Arguments = $"-ling {pathToSourceTxtFile} {outputFile}",
        };
        
        var process = new Process { StartInfo = startInfo };
        process.Start();
        
        process.WaitForExit();

        return File.ReadAllLines(outputFile);
    }
}

public record WordInfo
{
    public string Word { get; init; }
    public string PartOfSpeech { get; init; }
    public int NumberInText { get; init; }
    
    private readonly HashSet<string> partsOfSpeech = new()
    {
        "прилагательное",
        "наречие",
        "местоименное наречие",
        "числительное-прилагательное",
        "местоимение-прилагательное",
        "часть композита - сложного слова",
        "союз",
        "междометие",
        "числительное",
        "частица",
        "предлог",
        "существительное",
        "местоимение-существительное",
        "глагол"
    };
    
    public WordInfo(string word, string partOfSpeech, int numberInText)
    {
        Word = word;
        if (!partsOfSpeech.Contains(partOfSpeech))
            throw new ArgumentException("Некорректная чаcть речи", nameof(partOfSpeech));
        PartOfSpeech = partOfSpeech;
        NumberInText = numberInText;
    }
}