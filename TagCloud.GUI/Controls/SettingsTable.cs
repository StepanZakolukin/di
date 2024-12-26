using System.Collections.Generic;
using System.Windows.Forms;
using TagCloud.CloudLayout;
using TagCloud.ImageGeneration;

namespace TagCloudGUI.Controls;

public class SettingsTable : TableLayoutPanel
{
    public SettingsTable(IVisualizationProvider visualizationProvider, IEnumerable<IColorPicker> colorPickers,
        IEnumerable<ILayoutProvider> layoutProviders, TagCloudConfigurationForm parentForm)
    {
        Dock = DockStyle.Fill;
        RowStyles.Add(new RowStyle(SizeType.AutoSize));
        ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 348));
        ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 49));
        ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 523));
        
        Controls.Add(new FirstColumn(visualizationProvider, colorPickers, layoutProviders, parentForm), 0, 0);
        Controls.Add(new Panel {Dock = DockStyle.Fill}, 1, 0);
        Controls.Add(new SecondColumn(visualizationProvider, parentForm), 2, 0);
    }
}