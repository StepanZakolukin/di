using System.Collections;
using TagCloud.CloudLayout;
using TagCloud.ImageGeneration;

namespace TagCloudGUI.Controls;

public class SettingUpLayoutAlgorithm : DropdownList
{
    private Dictionary<string, ILayoutProvider> layoutProviders = new();
    public SettingUpLayoutAlgorithm(IVisualizationProvider visualizationProvider, IEnumerable<ILayoutProvider> layoutProviders)
        : base("Алгоритм генерации раскладки:", layoutProviders.Select(provider => provider.Name), visualizationProvider)
    {
        foreach (var provider in layoutProviders)
            this.layoutProviders[provider.Name] = provider;
        
        DropDownList.SelectedIndexChanged += LayoutProviderIsSelected;
    }

    private void LayoutProviderIsSelected(object? sender, EventArgs e)
    {
        var dropdownList = sender as ComboBox;
        VisualizationProvider.LayoutProvider = layoutProviders[dropdownList.SelectedItem.ToString()];
    }
}