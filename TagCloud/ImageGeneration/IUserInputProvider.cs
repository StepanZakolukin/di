using TagCloud.CloudLayout;
using TagCloud.TextProcessing;

namespace TagCloud.ImageGeneration;

public interface IUserInputProvider
{
    public IColorPicker ColorPicker { get; set; }
    public IEnumerable<WordInfo>? Words { get; set; }
    public ILayoutProvider LayoutProvider { get; set; }
}