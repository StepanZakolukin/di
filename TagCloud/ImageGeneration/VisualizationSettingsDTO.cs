using System.Drawing;

namespace TagCloud.ImageGeneration;

public class VisualizationSettingsDto
{
    public Size ImageSize { get; set; } = new(1080, 1080);
    public FontFamily FontFamily { get; set; } = new("Arial");
    
    private float cloudCompressionRatio = 0.8f;
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