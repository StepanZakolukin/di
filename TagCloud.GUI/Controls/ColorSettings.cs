using TagCloud.ImageGeneration;

namespace TagCloudGUI.Controls;

public class ColorSettings : DropdownList
{
    private readonly Dictionary<string, IColorPicker> coloringAlgorithms = new();
    public ColorSettings(IEnumerable<IColorPicker> coloringAlgorithms, IVisualizationProvider visualizationProvider) 
        : base("Алгоритм расцветки слов:",
            coloringAlgorithms.Select(colorPicker => colorPicker.Name),
            visualizationProvider)
    {
        foreach (var colorPicker in coloringAlgorithms)
            this.coloringAlgorithms[colorPicker.Name] = colorPicker;
        DropDownList.SelectedIndexChanged += ColoringAlgorithmsIsSelected;
    }

    private void ColoringAlgorithmsIsSelected(object sender, EventArgs e)
    {
        var dropdownList = sender as ComboBox;
        VisualizationProvider.ColorPicker = coloringAlgorithms[dropdownList.SelectedItem.ToString()];
    }
}