using TagCloud.ImageGeneration;

namespace TagCloudGUI.Controls;

public class SecondColumn : TableLayoutPanel
{
    private readonly WordFilter WordFilter;
    private readonly TagCloudConfigurationForm ParentForm;
    private readonly PartOfSpeechFilter PartOfSpeechFilter;
    public SecondColumn(TagCloudConfigurationForm parentForm)
    {
        Dock = DockStyle.Fill;
        ParentForm = parentForm;
        WordFilter = new WordFilter(parentForm);
        PartOfSpeechFilter = new PartOfSpeechFilter(parentForm);
        ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        for (var i = 0; i < 2; i++)
            RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        Controls.Add(PartOfSpeechFilter, 0, 0);
        Controls.Add(WordFilter, 0, 1);
        parentForm.SetupIsFinished += FilterData;
    }
    
    private void FilterData()
    {
        var excludedWords = WordFilter.GetSelectedValues().ToHashSet();
        var excludedPartsOfSpeech = PartOfSpeechFilter.GetSelectedValues().ToHashSet();
        ParentForm.FilterWords = ParentForm.Words
            .Where(word => !excludedPartsOfSpeech.Contains(word.PartOfSpeach) && !excludedWords.Contains(word.Word));
    }
}