using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;
using TagCloud.ImageGeneration;

namespace TagCloudGUI.Controls;

public class FontSettings : DropdownList
{
    private readonly IVisualizationProvider visualizationProvider;
    private static readonly IEnumerable<string> FontFamilies = new InstalledFontCollection().Families
        .Select(family => family.Name);
    
    public FontSettings(IVisualizationProvider visualizationProvider, TagCloudConfigurationForm parentForm) 
        : base("Шрифт:", FontFamilies, parentForm)
    {
        Dock = DockStyle.Fill;
        this.visualizationProvider = visualizationProvider;
        DropDownList.SelectedIndexChanged += FontIsSelected;
    }

    private void FontIsSelected(object? sender, EventArgs args)
    {
        var dropdownList = sender as ComboBox;
        visualizationProvider.FontFamily = new FontFamily(dropdownList.SelectedItem.ToString());
    }
}