using System.Drawing;
using TagCloud.TextProcessing;

namespace TagCloud.ImageGeneration;

public class ColorPicker : IColorPicker
{
    /*private readonly Color color;
    private readonly int totalNumberWords;

    public ColorPicker(Color color, int totalNumberWords)
    {
        if (this.color.A != 255)
            throw new ArgumentException("Цвет не должен быть прозрачным.", nameof(color));
        this.color = color;
        this.totalNumberWords = totalNumberWords;
    }*/

    public Color GetColorForWord(WordInfo word)
    {
        /*if (word.NumberInText > totalNumberWords)
            throw new ArgumentException($"{nameof(word.NumberInText)} в тексте превышает {nameof(totalNumberWords)}");
        
        return Color.FromArgb((int)(255 * (double)word.NumberInText / totalNumberWords), color);*/
        
        return Color.Red;
    }
}