using System.Drawing;
using Microsoft.Extensions.DependencyInjection;
using TagCloud.CloudLayout;
using TagCloud.ImageGeneration;
using TagCloud.ReadingFiles;
using TagCloud.TextProcessing;

namespace TagCloud.Tests;

[TestFixture]
public class VisualizationCloudLayoutTests
{
    private readonly IVisualizationProvider visualizationProvider;

    public VisualizationCloudLayoutTests()
    {
        var services = new ServiceCollection();
        services.AddSingleton<IReader, TxtReader>();
        services.AddSingleton<IReaderProvider, ReaderPicker>();
        services.AddSingleton<IColorPicker, ColorPicker>();
        services.AddSingleton<IWordsProvider, TextPreprocessing>();
        services.AddSingleton<IUserInputProvider, UserInputProvider>();
        services.AddSingleton<IVisualizationProvider, VisualizationCloudLayout>();
        services.AddSingleton<ISettingsProvider<VisualizationSettingsDto>, VisualizationSettings>();
        var imageSize = new Size(1080, 1080);
        services.AddSingleton<VisualizationSettingsDto>(_ => new VisualizationSettingsDto(
            imageSize,
            new FontFamily("Arial"),
            1.4f));
        services.AddTransient<ILayoutProvider>(_ => new CircularCloud(new Point(imageSize.Width / 2, imageSize.Height / 2)));

        var provider = services.BuildServiceProvider();
        visualizationProvider = provider.GetService<IVisualizationProvider>();
    }
    
    [Test]
    public void VisualizationCloudLayout_Test()
    {
        
    }
}