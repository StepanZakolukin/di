using System.Drawing;

namespace TagCloud.ImageGeneration;

public class VisualizationSettingsDTO
{
    public Size ImageSize { get; set; }
    public FontFamily FontFamily { get; set; }
    public float CloudCompressionRatio { get; set; }
}