using System.Drawing;
using TagCloud.TextProcessing;

namespace TagCloud.ImageGeneration;

public interface IColorPicker
{
    public Color GetColorForWord(WordInfo word);
}