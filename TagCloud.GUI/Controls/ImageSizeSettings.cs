using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagCloud.ImageGeneration;

namespace TagCloudGUI.Controls;

public class ImageSizeSettings : TableLayoutPanel
{
    private static readonly Padding margin = new Padding(0, 0, 0, 14);
    
    public Label Heading { get; set; } = new()
    {
        Dock = DockStyle.Fill,
        Text = "Размеры изображения:",
        Margin = margin,
    };

    private readonly Label widthLabel = new()
    {
        Text = "Ширина:",
        Dock = DockStyle.Fill,
        Margin = margin,
    };

    private readonly TextBox widthTextBox = new()
    {
        Dock = DockStyle.Fill,
        Margin = margin,
    };

    private readonly Label heightLabel = new()
    {
        Text = "Высота:",
        Dock = DockStyle.Fill,
        Margin = margin,
    };
    
    private readonly TextBox heightTextBox = new()
    {
        Dock = DockStyle.Fill,
        Margin = margin,
    };

    private readonly Label widthUnitsOfMeasurement = new()
    {
        Text = "px.",
        Dock = DockStyle.Fill,
        Margin = margin,
    };

    private readonly Label heightUnitsOfMeasurement = new()
    {
        Text = "px.",
        Dock = DockStyle.Fill,
        Margin = margin,
    };
    
    private readonly IVisualizationProvider visualizationProvider;
    
    public ImageSizeSettings(IVisualizationProvider visualizationProvider)
    {
        Dock = DockStyle.Fill;
        widthTextBox.Text = visualizationProvider.ImageSize.Width.ToString();
        heightTextBox.Text = visualizationProvider.ImageSize.Height.ToString();
        this.visualizationProvider = visualizationProvider;
        RowStyles.Add(new RowStyle(SizeType.AutoSize));
        RowStyles.Add(new RowStyle(SizeType.Percent, 66.66F));
        ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        
        Controls.Add(Heading, 0, 0);
        Controls.Add(CreateNestedTable(), 0, 1);

        heightTextBox.TextChanged += ProcessHeightChange;
        widthTextBox.TextChanged += ProcessWidthChange;
    }

    private void ProcessHeightChange(object sender, EventArgs args)
    {
        if (int.TryParse(heightTextBox.Text, out var height) && height > 0)
        {
            visualizationProvider.ImageSize = visualizationProvider.ImageSize with
            {
                Height = height,
            };
            heightTextBox.BackColor = Color.White;
        }
        else
        {
            heightTextBox.BackColor = Color.Red;
        }
    }
    
    private void ProcessWidthChange(object sender, EventArgs args)
    {
        if (int.TryParse(widthTextBox.Text, out var width) && width > 0)
        {
            visualizationProvider.ImageSize = visualizationProvider.ImageSize with
            {
                Width = width
            };
            widthTextBox.BackColor = Color.White;
        }
        else
        {
            widthTextBox.BackColor = Color.Red;
        }
    }

    private TableLayoutPanel CreateNestedTable()
    {
        var table = new TableLayoutPanel { Dock = DockStyle.Fill };
        for (var i = 0; i < 2; i++)
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 115));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 95));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 66));
        
        table.Controls.Add(widthLabel, 0, 0);
        table.Controls.Add(widthTextBox, 1, 0);
        table.Controls.Add(widthUnitsOfMeasurement, 2, 0);
        
        table.Controls.Add(heightLabel, 0, 1);
        table.Controls.Add(heightTextBox, 1, 1);
        table.Controls.Add(heightUnitsOfMeasurement, 2, 1);

        return table;
    }
}