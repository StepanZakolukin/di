using System.Drawing;
using System.Drawing.Text;
using TagCloud.CloudLayout;
using TagCloud.ImageGeneration;
using TagCloud.TextProcessing;

namespace TagCloud.Console;

public static class Program
{
    public static void Main()
    {
        /*var path = "FilesWithTexts/Колобок.txt";
        var preprocessor = new TextPreprocessing();
        var words = preprocessor.PerformPreprocessing(path);
        var visual = new VisualizationCloudLayout(new ColorPicker(), new CircularCloud(new Point(540, 540)), words);
        visual.CreateImage().Save("Images/Колобок.png");*/

        foreach (var font in new InstalledFontCollection().Families)
        {
            System.Console.WriteLine(font.Name);
        }
    }
}