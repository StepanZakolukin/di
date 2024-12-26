using System.Drawing;

namespace TagCloud.CloudLayout;

public interface ILayoutProvider
{
    public string Name { get; }
    public ILayoutProvider ResetLayout();
    public RectangleF PutNextRectangle(SizeF rectangleSize);
}