namespace TagCloudGUI.Controls;

public class MyButton : Button
{
    public MyButton()
    {
        FlatStyle = FlatStyle.Flat;
        Padding = new Padding(0);
        Margin = new Padding(0);
        Height = 40;
        TextAlign = ContentAlignment.TopCenter;
    }
}