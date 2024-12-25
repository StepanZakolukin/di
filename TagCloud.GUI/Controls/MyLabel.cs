namespace TagCloudGUI.Controls;

public class MyLabel : Label
{
    public MyLabel(string text)
    {
        Text = text;
        Dock = DockStyle.Fill;
        Margin = new Padding(0, 0, 0, 14);
    }
}