using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TagCloud.ImageGeneration;

namespace TagCloudGUI.Controls;

public class DropdownList : TableLayoutPanel
{
    private readonly MyLabel heading;
    protected readonly TagCloudConfigurationForm ParentForm;
    protected ComboBox DropDownList { get; init; } = new()
    {
        Margin = new Padding(0, 0, 0, 14),
        Dock = DockStyle.Fill,
    };
    
    public DropdownList(string heading, IEnumerable<string> list, TagCloudConfigurationForm parentForm)
    {
        Dock = DockStyle.Fill;
        ParentForm = parentForm;
        this.heading = new(heading);
        
        DropDownList.Items.AddRange(list.ToArray());
        
        ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        Controls.Add(this.heading, 0, 0);
        Controls.Add(DropDownList, 0, 1);
    }
}