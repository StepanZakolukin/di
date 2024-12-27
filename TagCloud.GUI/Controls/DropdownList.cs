namespace TagCloudGUI.Controls;

public class DropdownList : TableLayoutPanel
{
    private readonly MyLabel heading;
    protected readonly TagCloudConfigurationForm ParentForm;

    public DropdownList(string heading, IEnumerable<string> list, TagCloudConfigurationForm parentForm)
    {
        Dock = DockStyle.Fill;
        ParentForm = parentForm;
        this.heading = new MyLabel(heading);

        DropDownList.Items.AddRange(list.ToArray());

        ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        Controls.Add(this.heading, 0, 0);
        Controls.Add(DropDownList, 0, 1);
    }

    protected ComboBox DropDownList { get; init; } = new()
    {
        Margin = new Padding(0, 0, 0, 14),
        Dock = DockStyle.Fill
    };
}