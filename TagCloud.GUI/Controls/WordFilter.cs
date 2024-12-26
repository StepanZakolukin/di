using TagCloud.ImageGeneration;

namespace TagCloudGUI.Controls;

public class WordFilter : MyTreeView
{
    public WordFilter(IVisualizationProvider visualizationProvider, TagCloudConfigurationForm parentForm)
        : base("Исключить слова:", parentForm)
    {
        parentForm.TextIsUploaded += FillTreeView;
        ParentForm.SetupIsFinished += FilterData;
    }
    
    private void FillTreeView()
    {
        var words = ParentForm.Words.Select(wordInfo => wordInfo.Word);
        foreach (var word in words)
            TreeView.Nodes.Add(word);
    }
    
    private void FilterData()
    {
        var set = GetSelectedValues().ToHashSet();
        lock (ParentForm.Words)
        {
            ParentForm.Words = ParentForm.Words.Where(word => !set.Contains(word.Word));
        }
    }
}