using TagCloud.TextProcessing;

namespace TagCloud;

public interface IWordsProvider
{
    public IEnumerable<WordInfo> PerformPreprocessing(string pathToSourceTxtFile);
}