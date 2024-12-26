using TagCloud.ImageGeneration;

namespace TagCloudGUI.Controls;

public class SecondColumn : TableLayoutPanel
{
    public SecondColumn(IVisualizationProvider visualizationProvider, TagCloudConfigurationForm parentForm)
    {
        Dock = DockStyle.Fill;
        ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        for (var i = 0; i < 2; i++)
            RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        Controls.Add(new PartOfSpeechFilter(visualizationProvider, parentForm), 0, 0);
        Controls.Add(new WordFilter(visualizationProvider, parentForm), 0, 1);
    }
}