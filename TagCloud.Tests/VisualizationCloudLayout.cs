using System.Drawing;

namespace TagCloud.Tests;

public class VisualizationCloudLayout
{
    private readonly int width;
    private readonly int height;
    private readonly Color backgroundColor;
    private readonly Color[] rectanglePalette;

    public VisualizationCloudLayout(int width, int height, Color backgroundColor, IEnumerable<Color> rectanglePalette)
    {
        this.width = width;
        this.height = height;
        this.backgroundColor = backgroundColor;
        this.rectanglePalette = rectanglePalette.ToArray();
    }

    public Bitmap CreateImage(IEnumerable<RectangleF> rectangles)
    {
        var image = new Bitmap(width, height);

        DrawCloudLayout(Graphics.FromImage(image), rectangles);

        return image;
    }

    private void DrawCloudLayout(Graphics graphics, IEnumerable<RectangleF> rectangles)
    {
        var random = new Random();
        var array = rectangles.ToArray();
        var color = rectanglePalette[random.Next(rectanglePalette.Length)];

        graphics.FillRectangle(new SolidBrush(backgroundColor), 0, 0, width, height);
        graphics.FillRectangles(new SolidBrush(color), array);
        graphics.DrawRectangles(new Pen(Color.Black, 1), array);
    }
}
