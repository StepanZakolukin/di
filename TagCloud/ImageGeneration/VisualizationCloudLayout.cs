using System.Drawing;
using TagCloud.CloudLayout;
using TagCloud.TextProcessing;

namespace TagCloud.ImageGeneration;

public class VisualizationCloudLayout : IVisualizationProvider
{
    public VisualizationCloudLayout(ISettingsProvider<VisualizationSettingsDto> settingsProvider)
    {
        SettingsProvider = settingsProvider;
    }

    private float coefficient;
    public ISettingsProvider<VisualizationSettingsDto> SettingsProvider { get; }

    private IColorPicker ColorPicker { get; set; }
    private ILayoutProvider LayoutProvider { get; set; }
    private IEnumerable<WordInfo> WordsInfo { get; set; }

    public Bitmap CreateImage(IEnumerable<WordInfo> words, IColorPicker colorPicker, ILayoutProvider layoutProvider)
    {
        WordsInfo = words;
        ColorPicker = colorPicker;
        LayoutProvider = layoutProvider;
        coefficient = SettingsProvider.Settings.ImageSize.Width * SettingsProvider.Settings.CloudCompressionRatio / WordsInfo.Count();
        var image = new Bitmap(SettingsProvider.Settings.ImageSize.Width, SettingsProvider.Settings.ImageSize.Height);
        DrawСloudOfWords(Graphics.FromImage(image));

        return image;
    }

    private void DrawСloudOfWords(Graphics graphics)
    {
        foreach (var word in WordsInfo)
        {
            var color = ColorPicker.GetColorForWord(word);
            var height = word.NumberInText * coefficient;
            var font = new Font(SettingsProvider.Settings.FontFamily, height, GraphicsUnit.Pixel);
            var size = graphics.MeasureString(word.Word, font);
            var location = LayoutProvider.PutNextRectangle(size);

            graphics.DrawString(word.Word, font, new SolidBrush(color), location);
        }
    }
}