namespace TagCloud.TextProcessing;

public class WordInfo
{
    public string Word { get; init; }
    public string PartOfSpeech { get; init; }
    public int NumberInText { get; init; }
    
    private readonly HashSet<string> partsOfSpeech =
    [
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
    ];
    
    public WordInfo(string word, string partOfSpeech, int numberInText)
    {
        Word = word;
        if (!partsOfSpeech.Contains(partOfSpeech))
            throw new ArgumentException("Некорректная чаcть речи", nameof(partOfSpeech));
        PartOfSpeech = partOfSpeech;
        NumberInText = numberInText;
    }
}