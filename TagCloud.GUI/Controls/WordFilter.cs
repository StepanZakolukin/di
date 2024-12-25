using TagCloud.ImageGeneration;

namespace TagCloudGUI.Controls;

public class WordFilter : MyTreeView
{
    private readonly IVisualizationProvider visualizationProvider;
    
    public WordFilter(IVisualizationProvider visualizationProvider) : base("Исключить слова:")
    {
        this.visualizationProvider = visualizationProvider;
    }
}