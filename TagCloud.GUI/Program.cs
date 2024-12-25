using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using TagCloud.CloudLayout;
using TagCloud.ImageGeneration;
using TagCloud.TextProcessing;
using Point = TagCloud.CloudLayout.Point;

namespace TagCloudGUI;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        var services = new ServiceCollection();
        services.AddSingleton<IVisualizationProvider, VisualizationCloudLayout>();
        services.AddSingleton<IColorPicker, ColorPicker>();
        services.AddSingleton<ILayoutProvider, CircularCloud>();
        services.AddSingleton<IWordsProvider, TextPreprocessing>();
        services.AddSingleton<Form, TagCloudConfigurationForm>();
        
        var provider = services.BuildServiceProvider();
        var imageSize = provider.GetService<IVisualizationProvider>().ImageSize;
        services.AddScoped<Point>(_ => new Point(imageSize.Width / 2, imageSize.Height / 2));
        
        provider = services.BuildServiceProvider();
        var form = provider.GetService<Form>();
        Application.Run(form);
    }
}