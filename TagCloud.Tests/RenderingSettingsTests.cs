/*using System.Drawing;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using TagCloud.ImageGeneration;
using TagCloud.ImageGeneration.Settings;

namespace TagCloud.Tests;

[TestFixture]
public class RenderingSettingsTests
{
    private readonly RenderingSettings settings = new(
        new Size(1080, 1080),
        "Arial",
        5);

    [TestCase(-100, 100, "Arial", 5)]
    [TestCase(100, -100, "Arial", 5)]
    [TestCase(100, 0, "Arial", 5)]
    [TestCase(0, 100, "Arial", 5)]
    [TestCase(100, 100, "mmm", 5)]
    [TestCase(100, 100, "Arial", 0)]
    [TestCase(100, 100, "Arial", 11)]
    public void VisualizationSettingsDto_IncorrectInitialization_ThrowsException(
        int width, int height,
        string fontName,
        float coefficient)
    {
        var calling = () =>
            new RenderingSettings(new Size(width, height), fontName, coefficient);

        calling.Should().Throw<ArgumentException>();
    }
    
    [TestCase(100, 100, "Arial", 5)]
    public void VisualizationSettingsDto_CorrectInitialization_NotThrow(
        int width, int height,
        string fontName,
        float coefficient)
    {
        var calling = () =>
            new RenderingSettings(new Size(width, height), fontName, coefficient);

        calling.Should().NotThrow();
    }

    [TestCase(0)]
    [TestCase(-1000f)]
    [TestCase(1000f)]
    [TestCase(10.01f)]
    public void SetValueCloudCompressionRatio_IncorrectValue(float coefficient)
    {
        var expected = settings.CloudCompressionRatio;
        var status = settings.SetValueCloudCompressionRatio(coefficient);

        status.IsSuccess.Should().BeFalse();
        settings.CloudCompressionRatio.Should().Be(expected);
    }
    
    [TestCase(2)]
    [TestCase(10f)]
    [TestCase(0.1f)]
    public void SetValueCloudCompressionRatio_CorrectValue(float coefficient)
    {
        var status = settings.SetValueCloudCompressionRatio(coefficient);

        status.IsSuccess.Should().BeTrue();
        settings.CloudCompressionRatio.Should().Be(coefficient);
    }

    [TestCase(540, 200)]
    [TestCase(100, 1080)]
    public void SetValueImageSize_PositiveValues(int width, int height)
    {
        var status = settings.SetValueImageSize(new Size(width, height));
        
        status.IsSuccess.Should().BeTrue();
        settings.ImageSize.Should().Be(new Size(width, height));
    }
    
    [TestCase(-100, 100)]
    [TestCase(100, -100)]
    [TestCase(0, 100)]
    [TestCase(100, 0)]
    public void SetValueImageSize_NotPositiveValues(int width, int height)
    {
        var expected = settings.ImageSize;
        var status = settings.SetValueImageSize(new Size(width, height));
        
        status.IsSuccess.Should().BeFalse();
        settings.ImageSize.Should().Be(expected);
    }
    
    [TestCase("ms")]
    public void SetValueFontFamily_NonExistentFont(string fontName)
    {
        var expected = settings.FontFamily.Name;
        var status = settings.SetValueFontFamily(fontName);
        
        status.IsSuccess.Should().BeFalse();
        settings.FontFamily.Name.Should().Be(expected);
    }

    [TestCase("Arial")]
    public void SetValueFontFamily_SystemFont(string fontName)
    {
        var status = settings.SetValueFontFamily(fontName);
        
        status.IsSuccess.Should().BeTrue();
        settings.FontFamily.Name.Should().Be(fontName);
    }
}*/