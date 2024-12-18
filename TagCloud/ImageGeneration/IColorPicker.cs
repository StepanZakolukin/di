using System.Drawing;
using TagCloud.TextProcessing;

namespace TagCloud;

public interface IColorPicker
{
    public Color GetColorForWord(WordInfo word);
}