using System.Drawing;
using TagCloud.CloudLayout;
using TagCloud.ImageGeneration;
using TagCloud.TextProcessing;
using Point = TagCloud.CloudLayout.Point;

namespace TagCloud.Console;

public static class Program
{
    public static void Main()
    {
        var path = "C:/Users/stepa/Desktop/Mystem/in.txt";
        var preprocessor = new TextPreprocessing();
        var words = preprocessor.PerformPreprocessing(path);
        var visual = new VisualizationCloudLayout();
        visual.ImageSize = new Size(1080, 1080);
        for (var i = 0; i < 5; i++)
            visual.CreateImage(words, new ColorPicker(), new CircularCloud(new Point(540, 540)))
                .Save($"C:/Users/stepa/Desktop/Колобок{i}.png");
    }
}