using System.Drawing;
using TagCloud.CloudLayout;
using TagCloud.TextProcessing;

namespace TagCloud.ImageGeneration;

public class VisualizationCloudLayout : IVisualizationProvider
{
    private float coefficient;
    private IColorPicker ColorPicker { get; set; }
    private ILayoutProvider LayoutProvider { get; set; }
    private IEnumerable<WordInfo> WordsInfo { get; set; }
    public Size ImageSize { get; set; } = new(1080, 1080);
    public FontFamily FontFamily { get; set; } = new("Arial");

    private float cloudCompressionRatio;
    public float CloudCompressionRatio
    {
        get => cloudCompressionRatio;
        set
        {
            if (value < 0.001 || value > 1.001)
                throw new ArgumentException("Должно быть больше 0, но меньше или равно единице", nameof(value));
            
            cloudCompressionRatio = value;
        }
    }
    
    public VisualizationCloudLayout()
    {
        CloudCompressionRatio = 0.8f;
    }

    public Bitmap CreateImage(IEnumerable<WordInfo> words, IColorPicker colorPicker, ILayoutProvider layoutProvider)
    {
        WordsInfo = words;
        ColorPicker = colorPicker;
        LayoutProvider = layoutProvider;
        coefficient = ImageSize.Width * cloudCompressionRatio / WordsInfo.Count();
        var image = new Bitmap(ImageSize.Width, ImageSize.Height);
        DrawСloudOfWords(Graphics.FromImage(image));

        return image;
    }

    private void DrawСloudOfWords(Graphics graphics)
    {
        foreach (var word in WordsInfo)
        {
            var color = ColorPicker.GetColorForWord(word);
            var height = word.NumberInText * coefficient;
            var font = new Font(FontFamily, height * 5, GraphicsUnit.Pixel);
            var size = graphics.MeasureString(word.Word, font);
            var location = LayoutProvider.PutNextRectangle(size);

            graphics.DrawString(word.Word, font, new SolidBrush(color), location);
        }
    }
}
