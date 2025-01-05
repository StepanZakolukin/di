using TagCloud.CloudLayout;
using TagCloud.TextProcessing;

namespace TagCloud.ImageGeneration;

public class UserInputProvider : IUserInputProvider
{
    public UserInputProvider(IColorPicker colorPicker, ILayoutProvider layoutProvider)
    {
        ColorPicker = colorPicker;
        LayoutProvider = layoutProvider;
    }
    
    private IEnumerable<WordInfo>? words;

    public IEnumerable<WordInfo>? Words
    {
        get => words;
        set => words = value ?? throw new ArgumentNullException(nameof(value));
    }

    private IColorPicker colorPicker;
    public IColorPicker ColorPicker
    {
        get => colorPicker;
        set => colorPicker = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    private ILayoutProvider layoutProvider;
    public ILayoutProvider LayoutProvider
    {
        get => layoutProvider;
        set => layoutProvider = value ?? throw new ArgumentNullException(nameof(value));
    }
}