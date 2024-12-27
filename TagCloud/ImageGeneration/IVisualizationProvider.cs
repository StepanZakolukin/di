using System.Drawing;
using TagCloud.CloudLayout;
using TagCloud.TextProcessing;

namespace TagCloud.ImageGeneration;

public interface IVisualizationProvider
{
    public Size ImageSize { get; set; }
    public FontFamily FontFamily { get; set; }
    public float CloudCompressionRatio { get; set; }
    public Bitmap CreateImage(IEnumerable<WordInfo> words, IColorPicker colorPicker, ILayoutProvider layoutProvider);
}