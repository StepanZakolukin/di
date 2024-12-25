using System.Drawing;

namespace TagCloud.CloudLayout;

public interface ILayoutProvider
{
    public string Name { get; }
    public RectangleF PutNextRectangle(SizeF rectangleSize);
}