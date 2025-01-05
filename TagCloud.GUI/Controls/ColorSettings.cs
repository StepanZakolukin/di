using TagCloud.ImageGeneration;

namespace TagCloudGUI.Controls;

public class ColorSettings : DropdownList
{
    private readonly Dictionary<string, IColorPicker> coloringAlgorithms = new();

    public ColorSettings(IEnumerable<IColorPicker> coloringAlgorithms,
        TagCloudConfigurationForm parentForm) : base("Алгоритм расцветки слов:",
        coloringAlgorithms.Select(colorPicker => colorPicker.Name), parentForm)
    {
        foreach (var colorPicker in coloringAlgorithms)
            this.coloringAlgorithms[colorPicker.Name] = colorPicker;
        
        DropDownList.SelectedItem = parentForm.ColorPicker?.Name;
        DropDownList.SelectedIndexChanged += ColoringAlgorithmsIsSelected;
    }

    private void ColoringAlgorithmsIsSelected(object? sender, EventArgs e)
    {
        var dropdownList = sender as ComboBox;
        ParentForm.ColorPicker = coloringAlgorithms[dropdownList.SelectedItem.ToString()];
    }
}