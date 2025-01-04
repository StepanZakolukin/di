namespace TagCloud.ImageGeneration;

public class VisualizationSettings : ISettingsProvider<VisualizationSettingsDto>
{
    public VisualizationSettingsDto Settings { get; } = new();
}