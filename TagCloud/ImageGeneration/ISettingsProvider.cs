namespace TagCloud.ImageGeneration;

public interface ISettingsProvider<TSettings>
{
    public VisualizationSettingsDto Settings { get; }
}