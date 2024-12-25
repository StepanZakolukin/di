namespace TagCloudGUI.Controls;

public class MyTreeView : TableLayoutPanel
{
    private readonly MyLabel heading;
    
    protected readonly TreeView TreeView = new()
    {
        Dock = DockStyle.Fill,
    };
    
    public MyTreeView(string heading)
    {
        Dock = DockStyle.Fill;
        this.heading = new(heading);
        ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        RowStyles.Add(new RowStyle(SizeType.Absolute, 54));
        RowStyles.Add(new RowStyle(SizeType.Absolute, 270));
        
        Controls.Add(this.heading, 0, 0);
        Controls.Add(TreeView, 0 , 1);
    }
}