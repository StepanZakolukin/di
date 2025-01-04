using Microsoft.Extensions.DependencyInjection;
using TagCloud.CloudLayout;
using TagCloud.ImageGeneration;
using TagCloud.TextProcessing;
using Point = System.Drawing.Point;

namespace TagCloudGUI;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        var services = new ServiceCollection();
        services.AddSingleton<IVisualizationProvider, VisualizationCloudLayout>();
        services.AddSingleton<IColorPicker, ColorPicker>();
        services.AddSingleton<IWordsProvider, TextPreprocessing>();
        services.AddSingleton<ISettingsProvider<VisualizationSettingsDto>, VisualizationSettings>();
        services.AddSingleton<Form, TagCloudConfigurationForm>();

        var provider = services.BuildServiceProvider();
        var imageSize = provider.GetService<IVisualizationProvider>().SettingsProvider.Settings.ImageSize;
        services.AddSingleton<ILayoutProvider>(_ => new CircularCloud(new Point(imageSize.Width / 2, imageSize.Height / 2)));

        provider = services.BuildServiceProvider();
        var form = provider.GetService<Form>();
        Application.Run(form);
    }
}