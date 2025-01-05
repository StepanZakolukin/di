using System.Drawing;

namespace TagCloud.ImageGeneration;

public class VisualizationSettingsDto(Size imageSize, FontFamily fontFamily, float cloudCompressionRatio)
{
    public Size ImageSize { get; set; } = imageSize;
    public FontFamily FontFamily { get; set; } = fontFamily;
    
    private float cloudCompressionRatio = cloudCompressionRatio;
    public float CloudCompressionRatio
    {
        get => cloudCompressionRatio;
        set
        {
            if (value < 0.501 || value > 2.001)
                throw new ArgumentException("Должно быть больше 0.5, но меньше или равно 2", nameof(value));

            cloudCompressionRatio = value;
        }
    }
}