using System.Diagnostics;
using System.Text;
using TagCloudGUI.Controls;

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

    private IEnumerable<WordInfo> PerformPreprocessingOfWordFile(string fileWithWords)
    {
        const string partOfSpeach = "нет данных";
        var countingDictionary = new Dictionary<string, int>();
        
        using (var reader = new StreamReader(fileWithWords))
        {
            while (reader.ReadLine() is { } line)
            {
                var word = line.Trim();
                if (word == string.Empty) continue;
                if (!countingDictionary.TryAdd(word, 1))
                    countingDictionary[word]++;
            }
        }
        
        return countingDictionary.Select(pair => new WordInfo(pair.Key, partOfSpeach, pair.Value));
    }

    private IEnumerable<WordInfo> PerformPreliminaryProcessingOfLiteraryText(string pathToSourceTxtFile)
    {
        var textInfo = ParseText(pathToSourceTxtFile);
        var countingDictionary = new Dictionary<Tuple<string, string>, int>();

        foreach (var line in textInfo)
        {
            var info = line.Split('=');
            if (info.Length < 2 || info[0].Contains('?')) continue;
            var wordAndPartOfSpeech = Tuple.Create(info[0], info[1].Split(',')[0]);
            if (!countingDictionary.TryAdd(wordAndPartOfSpeech, 1))
                countingDictionary[wordAndPartOfSpeech]++;
        }

        return countingDictionary.Select(pair =>
            new WordInfo(pair.Key.Item1,
                decryptionGrammems[pair.Key.Item2],
                pair.Value));
    }
    
    public IEnumerable<WordInfo> PerformPreprocessing(string pathToSourceTxtFile, FileContentStructure structureOfContent)
    {
        if (!File.Exists(pathToSourceTxtFile))
            throw new FileNotFoundException();
        
        if (structureOfContent == FileContentStructure.Literary)
            return PerformPreliminaryProcessingOfLiteraryText(pathToSourceTxtFile);
        return PerformPreprocessingOfWordFile(pathToSourceTxtFile);
    }

    private IEnumerable<string> ParseText(string pathToSourceTxtFile)
    {
        var startInfo = new ProcessStartInfo
        {
            CreateNoWindow = true,
            UseShellExecute = false,
            RedirectStandardInput = false,
            RedirectStandardOutput = true,
            FileName = "TextProcessing/Mystem.exe",
            Arguments = $"-ling {pathToSourceTxtFile}",
        };

        var process = new Process { StartInfo = startInfo };
        process.Start();
        
        using (var reader = new StreamReader(process.StandardOutput.BaseStream, Encoding.UTF8))
        {
            while (reader.ReadLine() is { } line)
                yield return line;
        }
        
        process.WaitForExit();
    }

}