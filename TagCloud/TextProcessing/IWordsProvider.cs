namespace TagCloud.TextProcessing;

public interface IWordsProvider
{
    public IEnumerable<WordInfo> PerformPreprocessing(string pathToSourceTxtFile);
}