using System.Diagnostics;

namespace TagCloud.TextProcessing;

public class TextPreprocessing : IWordsProvider
{
    private readonly Dictionary<string, string> decryptionGrammems = new()
    {
        { "A", "прилагательное" },
        { "ADV", "наречие" },
        { "ADVPRO", "местоименное наречие" },
        { "ANUM", "числительное-прилагательное" },
        { "APRO", "местоимение-прилагательное" },
        { "COM", "часть композита - сложного слова" },
        { "CONJ", "союз" },
        { "INTJ", "междометие" },
        { "NUM", "числительное" },
        { "PART", "частица" },
        { "PR", "предлог" },
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
            if (info.Length < 2) continue;
            var wordAndPartOfSpeech = Tuple.Create(info[0], info[1].Split(',')[0]);
            if (!countingDictionary.TryAdd(wordAndPartOfSpeech, 1))
                countingDictionary[wordAndPartOfSpeech]++;
        }

        return countingDictionary.Select(pair =>
            new WordInfo(pair.Key.Item1,
                decryptionGrammems[pair.Key.Item2],
                pair.Value));
    }

    private string[] ParseText(string pathToSourceTxtFile)
    {
        var outputFile = "out.txt";
        File.Create(outputFile).Close();

        var startInfo = new ProcessStartInfo
        {
            UseShellExecute = false,
            RedirectStandardInput = false,
            RedirectStandardOutput = false,
            FileName = "TextProcessing/Mystem.exe",
            Arguments = $"-ling {pathToSourceTxtFile} {outputFile}"
        };

        var process = new Process { StartInfo = startInfo };
        process.Start();

        process.WaitForExit();

        return File.ReadAllLines(outputFile);
    }
}