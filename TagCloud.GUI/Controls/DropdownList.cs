using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TagCloud.ImageGeneration;

namespace TagCloudGUI.Controls;

public class DropdownList : TableLayoutPanel
{
    private static readonly Padding margin = new(0, 0, 0, 14);
    protected readonly IVisualizationProvider VisualizationProvider;
    
    private readonly Label heading = new()
    {
        Margin = margin,
        Dock = DockStyle.Fill,
    };
    protected ComboBox DropDownList { get; init; } = new()
    {
        Margin = margin,
        Dock = DockStyle.Fill,
    };
    
    public DropdownList(string heading, IEnumerable<string> list, IVisualizationProvider visualizationProvider)
    {
        Dock = DockStyle.Fill;
        this.heading.Text = heading;
        VisualizationProvider = visualizationProvider;
        
        DropDownList.Items.AddRange(list.ToArray());
        
        ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        Controls.Add(this.heading, 0, 0);
        Controls.Add(DropDownList, 0, 1);
    }
}