using System;
using System.Drawing;
using System.Windows.Forms;
using TagCloud.ImageGeneration;

namespace TagCloudGUI.Controls;

public class ConfiguringCloudCompressionRatio : TableLayoutPanel
{
    private readonly MyLabel heading = new("Коэф. сжатия облака:")
    {
        TextAlign = ContentAlignment.MiddleLeft,
    };

    private readonly TextBox coefficient = new()
    {
        Dock = DockStyle.Fill,
        TextAlign = HorizontalAlignment.Center,
    };
    
    private readonly IVisualizationProvider visualizationProvider;
    
    public ConfiguringCloudCompressionRatio(IVisualizationProvider visualizationProvider)
    {
        Dock = DockStyle.Fill;
        this.visualizationProvider = visualizationProvider;
        coefficient.Text = $"{Math.Round(visualizationProvider.CloudCompressionRatio, 2)}";
        coefficient.TextChanged += CoefficientHasChanged;
        
        RowStyles.Add(new RowStyle(SizeType.AutoSize));
        ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 260));
        ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 87));
        Controls.Add(heading, 0, 0);
        Controls.Add(coefficient, 1, 0);
    }

    private void CoefficientHasChanged(object? sender, EventArgs e)
    {
        if (float.TryParse(coefficient.Text, out var number) && number > 0.001 && number < 1.001)
        {
            coefficient.BackColor = Color.White;
            visualizationProvider.CloudCompressionRatio = number;
        }
        else
        {
            coefficient.BackColor = Color.Red;
        }
    }
}