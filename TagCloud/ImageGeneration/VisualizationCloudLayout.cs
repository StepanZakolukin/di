using System.Drawing;

namespace TagCloud.ImageGeneration;

public class VisualizationCloudLayout : IVisualizationProvider
{
    private float coefficient;

    public ISettingsProvider<VisualizationSettingsDto> SettingsProvider { get; }
    public VisualizationCloudLayout(ISettingsProvider<VisualizationSettingsDto> settingsProvider, IUserInputProvider userInputProvider)
    {
        SettingsProvider = settingsProvider;
        UserInputProvider = userInputProvider;
    }

    public IUserInputProvider UserInputProvider { get; init; }

    public Bitmap CreateImage()
    {
        coefficient = SettingsProvider.Settings.ImageSize.Width * SettingsProvider.Settings.CloudCompressionRatio / UserInputProvider.Words.Count();
        var image = new Bitmap(SettingsProvider.Settings.ImageSize.Width, SettingsProvider.Settings.ImageSize.Height);
        DrawСloudOfWords(Graphics.FromImage(image));

        return image;
    }

    private void DrawСloudOfWords(Graphics graphics)
    {
        foreach (var word in UserInputProvider.Words)
        {
            var color = UserInputProvider.ColorPicker.GetColorForWord(word);
            var height = word.NumberInText * coefficient;
            var font = new Font(SettingsProvider.Settings.FontFamily, height, GraphicsUnit.Pixel);
            var size = graphics.MeasureString(word.Word, font);
            var location = UserInputProvider.LayoutProvider.PutNextRectangle(size);

            graphics.DrawString(word.Word, font, new SolidBrush(color), location);
        }
    }
}