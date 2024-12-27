using System.Drawing;
using TagCloud.TextProcessing;

namespace TagCloud.ImageGeneration;

public class ColorPicker : IColorPicker
{
    public string Name { get; } = "Однотонный красный";

    public Color GetColorForWord(WordInfo word)
    {
        return Color.Red;
    }
}