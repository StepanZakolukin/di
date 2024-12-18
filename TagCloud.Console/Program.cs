using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;
using TagCloud.CloudLayout;
using TagCloud.TextProcessing;
using TagCloud.Visualization;

namespace TagCloud.Console;

internal class Program
{
    private const string Path = "../../../Images";

    static void Main()
    {
        /*var colors = new[] { Color.Red, Color.Green, Color.Brown, Color.Yellow, Color.Blue };

        var visual = new VisualizationCloudLayout(800, 600, Color.White, colors);

        visual.CreateImage(new CircularCloud(new Point(400, 300)), 175, new Size(30, 5), new Size(100, 25))
            .Save($"{Path}/CentralСloud.png");

        visual.CreateImage(new CircularCloud(new Point(250, 150)), 50, new Size(30, 5), new Size(80, 25))
            .Save($"{Path}/SmalСloud.png");*/
    }
}