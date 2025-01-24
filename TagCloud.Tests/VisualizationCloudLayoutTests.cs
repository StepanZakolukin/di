using System.Drawing;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using TagCloud.ImageGeneration;
using TagCloud.ImageGeneration.Settings;
using TagCloud.Parsing;
using TagCloud.ReadingFiles;

namespace TagCloud.Tests;

[TestFixture]
public class VisualizationCloudLayoutTests
{
    private IParserProvider wordsProvider;
    private IReaderProvider readerProvider;
    private IVisualizationProvider visualizationProvider;
    private readonly HashSet<string> partOfSpeechForFiltering =
    [
        "местоимение-прилагательное",
        "союз",
        "междометие",
        "частица",
        "предлог",
        "местоимение-существительное",
    ];

    public VisualizationCloudLayoutTests()
    {
        PrepareEnvironment();
    }
    
    [Test]
    public void VisualizationCloudLayout_ChangeSettings_SettingsShouldChange()
    {
        const float cloudCompressionRatio = 1.5f;
        var imageSize = new Size(1920, 540);
        var fontName = "Calibri";
        
        visualizationProvider.Settings.Settings.SetValueImageSize(imageSize);
        visualizationProvider.Settings.Settings.SetValueFontFamily(fontName);
        visualizationProvider.Settings.Settings.SetValueCloudCompressionRatio(cloudCompressionRatio);
        
        visualizationProvider.Settings.Settings.ImageSize.Should().Be(imageSize);
        visualizationProvider.Settings.Settings.FontFamily.Name.Should().Be(fontName);
        visualizationProvider.Settings.Settings.CloudCompressionRatio.Should().Be(cloudCompressionRatio);
    }
    
    [TestCase("Morozko.txt", "Morozko.jpeg",
        ContentStructure.Literary, 2.6f)]
    [TestCase("GeeseAndSwans.txt", "GeeseAndSwans.png",
        ContentStructure.Literary, 2.8f)]
    [TestCase("CheckingCount.txt", "CheckingCount.bmp",
        ContentStructure.ListOfWords, 0.4f)]
    public void CreateImage_ImageSizeMustMatchSettings(string fileName, string imageName,
        ContentStructure structure, float cloudCompressionRatio)
    {
        var sourceFile = Path.Combine("TestsFiles", fileName);
        var words = wordsProvider.GetParser(
            readerProvider.GetReader(sourceFile).GetValueOrThrow(), structure);
        visualizationProvider.UserInputProvider.SetValueListOfWords(
            words
                .Where(info => !partOfSpeechForFiltering.Contains(info.PartOfSpeach)));
        visualizationProvider.Settings.Settings.SetValueCloudCompressionRatio(cloudCompressionRatio);
        
        visualizationProvider.Settings.Settings.SetValueImageSize(new Size(1080, 1080));
        CheckSizeMatching(imageName);
        visualizationProvider.Settings.Settings.SetValueImageSize(new Size(1280, 720));
        CheckSizeMatching(imageName);
    }

    private void CheckSizeMatching(string imageName)
    {
        var image = GenerateImage(imageName);
        image.Size.Should().Be(visualizationProvider.Settings.Settings.ImageSize);
    }

    private Bitmap GenerateImage(string imageName)
    {
        imageName = $"({visualizationProvider.Settings.Settings.ImageSize.Width}" +
                    $"x{visualizationProvider.Settings.Settings.ImageSize.Height})" +
                    $"{imageName}";
        var pathToImage =  $"../../../Images/{imageName}";

        var image = visualizationProvider.CreateImage().GetValueOrThrow();
        image.Save(pathToImage);

        return image;
    }

    [TestCase("CheckingCount.txt", "CheckingCount.bmp",
    ContentStructure.ListOfWords, 10f)]
    public void CreateImage_FailedSettings_ImageWillNotBeGenerated(string fileName, string imageName,
        ContentStructure structure, float cloudCompressionRatio)
    {
        var sourceFile = Path.Combine("TestsFiles", fileName);
        var words = wordsProvider.GetParser(
            readerProvider.GetReader(sourceFile).GetValueOrThrow(), structure);
        visualizationProvider.UserInputProvider.SetValueListOfWords(words);
        
        var status = visualizationProvider.CreateImage();
        
        status.IsSuccess.Should().BeFalse();
    }

    [Test]
    public void CreateImage_WordsAreNotLoaded_ImageWillNotBeGenerated()
    {
        var status = visualizationProvider.CreateImage();
        
        status.IsSuccess.Should().BeFalse();
    }

    [TearDown]
    public void PrepareEnvironment()
    {
        var services = new ServiceCollection();
        services.AddSingleton<IReader, TxtReader>();
        services.AddSingleton<IReaderProvider, ReaderPicker>();
        services.AddSingleton<IParserProvider, LiteraryTextParser>();
        services.AddSingleton<IColorProvider, ColorPicker>();
        services.AddSingleton<IUserInputProvider, UserInputProvider>();
        services.AddSingleton<IVisualizationProvider, VisualizationCloudLayout>();
        services.AddSingleton<ISettingsProvider<RenderingSettings>, RenderingSettings>();
        var imageSize = new Size(1080, 1080);
        services.AddSingleton<RenderingSettings>(_ => new RenderingSettings(
            imageSize,
            "Arial",
            1f));
        services.AddTransient<ILayoutProvider>(_ => new CircularCloud(new Point(imageSize.Width / 2, imageSize.Height / 2)));

        var provider = services.BuildServiceProvider();
        
        wordsProvider = provider.GetService<IParserProvider>();
        readerProvider = provider.GetService<IReaderProvider>();
        visualizationProvider = provider.GetService<IVisualizationProvider>();
    }
}