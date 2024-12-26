namespace TagCloudGUI.Controls;

public class MyTreeView : TableLayoutPanel
{
    protected readonly TagCloudConfigurationForm ParentForm;
    private readonly MyLabel heading;
    
    protected readonly TreeView TreeView = new()
    {
        Dock = DockStyle.Fill,
        BorderStyle = BorderStyle.None,
        CheckBoxes = true,
        ShowLines = false,
    };
    
    public MyTreeView(string heading, TagCloudConfigurationForm parentForm)
    {
        ParentForm = parentForm;
        Dock = DockStyle.Fill;
        this.heading = new MyLabel(heading);
        ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        RowStyles.Add(new RowStyle(SizeType.Absolute, 54));
        RowStyles.Add(new RowStyle(SizeType.Absolute, 270));
        
        Controls.Add(this.heading, 0, 0);
        Controls.Add(TreeView, 0 , 1);
    }
    
    public IEnumerable<string> GetSelectedValues()
    {
        for (var i = 0; i < TreeView.Nodes.Count; i++)
        {
            if (TreeView.Nodes[i].IsSelected)
                yield return TreeView.Nodes[i].Text;
        }
    }
}