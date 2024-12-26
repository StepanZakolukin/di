using TagCloud.ImageGeneration;

namespace TagCloudGUI.Controls;

public class PartOfSpeechFilter : MyTreeView
{
    public PartOfSpeechFilter(IVisualizationProvider visualizationProvider, TagCloudConfigurationForm parentForm)
        : base("Исключить части речи:", parentForm)
    {
        TreeView.Margin = new Padding(0, 0, 0, 14);

        ParentForm.SetupIsFinished += FilterData;
        parentForm.TextIsUploaded += FillTreeView;
    }

    private void FillTreeView()
    {
        var partsOfSpeech = ParentForm.Words?
            .Select(wordInfo => wordInfo.PartOfSpeech)
            .ToHashSet();
        foreach (var partOfSpeech in partsOfSpeech)
            TreeView.Nodes.Add(partOfSpeech);
    }

    private void FilterData()
    {
        var set = GetSelectedValues().ToHashSet();
        lock (ParentForm.Words)
        {
            ParentForm.Words = ParentForm.Words.Where(word => !set.Contains(word.PartOfSpeech));
        }
    }
}