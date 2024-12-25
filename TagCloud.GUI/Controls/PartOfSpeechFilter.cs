using TagCloud.ImageGeneration;

namespace TagCloudGUI.Controls;

public class PartOfSpeechFilter : MyTreeView
{
    private readonly IVisualizationProvider visualizationProvider;
    
    public PartOfSpeechFilter(IVisualizationProvider visualizationProvider) : base("Исключить части речи:")
    {
        this.visualizationProvider = visualizationProvider;
        TreeView.Margin = new Padding(0, 0, 0, 14);
    }
}