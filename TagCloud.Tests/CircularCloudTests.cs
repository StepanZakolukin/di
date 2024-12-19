using System.Drawing;
using FluentAssertions;
using NUnit.Framework.Interfaces;
using TagCloud.CloudLayout;

namespace TagCloud.Tests;


[TestFixture]
public class CircularCloudTests
{
    private readonly List<RectangleF> listRectangles = [];

    [Test]
    public void CircularCloud_CorrectInitialization_NoExceptions()
    {
        var createAConstructor = () => new CircularCloud(new Point(960, 540));

        createAConstructor
            .Should()
            .NotThrow();
    }

    [Test]
    public void PutNextRectangle_RandomSizes_MustBeRightSize()
    {
        var random = new Random();
        var cloud = new CircularCloud(new Point(960, 540));

        for (var i = 0; i < 50; i++)
        {
            var width = random.Next(30, 200);
            var actualSize = new SizeF(width, random.Next(width / 6, width / 3));

            var rectangle = cloud.PutNextRectangle(actualSize);

            actualSize.Should().Be(rectangle.Size);
        }
    }

    [Test]
    public void PutNextRectangle_RandomSizes_ShouldNotIntersect()
    {
        var random = new Random();
        var cloudLayouter = new CircularCloud(new Point(960, 540));
        
        for (var i = 0; i < 100; i++)
        {
            var width = random.Next(30, 200);

            var rectangle = cloudLayouter.PutNextRectangle(new SizeF(width, random.Next(width / 6, width / 3)));

            listRectangles.Any(rect => rect.IntersectsWith(rectangle))
                .Should()
                .BeFalse("Прямоугольники не должны пересекаться");

            listRectangles.Add(rectangle);
        }
    }
}
