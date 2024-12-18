using System.Drawing;

namespace TagCloud.CloudLayout;

public interface ILayoutProvider
{
    public RectangleF PutNextRectangle(SizeF rectangleSize);
}