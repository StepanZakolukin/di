using System.Drawing;
using TagCloud.CloudLayout;
using TagCloud.TextProcessing;

namespace TagCloud.ImageGeneration;

public class VisualizationCloudLayout
{
    private readonly int numberOfWords;
    private float coefficient;
    private readonly IColorPicker colorPicker;
    private readonly ILayoutProvider layoutProvider;
    private readonly IEnumerable<WordInfo> wordsInfo;
    public Size ImageSize { get; set; } = new(1080, 1080);
    public FontFamily FontFamily { get; set; } = new("Arial");

    private float cloudCompressionRatio;
    public float CloudCompressionRatio
    {
        get => cloudCompressionRatio;
        set
        {
            if (1 - value < 0.01 || value < 0.01)
                throw new ArgumentException("Должно быть больше 0, но меньше или равно единице", nameof(value));
            
            cloudCompressionRatio = value;
            coefficient = ImageSize.Width * cloudCompressionRatio / numberOfWords;
        }
    }
    
    public VisualizationCloudLayout(IColorPicker colorPicker,
        ILayoutProvider layoutProvider, IEnumerable<WordInfo> words)
    {
        wordsInfo = words;
        numberOfWords = words.Count();
        CloudCompressionRatio = 0.8f;
        this.colorPicker = colorPicker;
        this.layoutProvider = layoutProvider;
    }

    public Bitmap CreateImage()
    {
        var image = new Bitmap(ImageSize.Width, ImageSize.Height);
        DrawСloudOfWords(Graphics.FromImage(image));

        return image;
    }

    private void DrawСloudOfWords(Graphics graphics)
    {
        foreach (var word in wordsInfo)
        {
            var color = colorPicker.GetColorForWord(word);
            var height = word.NumberInText * coefficient;
            var font = new Font(FontFamily, height, GraphicsUnit.Pixel);
            var size = graphics.MeasureString(word.Word, font);
            var location = layoutProvider.PutNextRectangle(size);

            graphics.DrawString(word.Word, font, new SolidBrush(color), location);
        }
    }
}
