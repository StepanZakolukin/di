using TagCloud.CloudLayout;
using TagCloud.ImageGeneration;
using TagCloud.TextProcessing;
using TagCloudGUI.Controls;

namespace TagCloudGUI;

public partial class TagCloudConfigurationForm : Form
{
    private IColorPicker? colorPicker;

    private bool everythingIsPrepared;
    public IEnumerable<WordInfo>? FilterWords;

    private ILayoutProvider? layoutProvider;
    private IEnumerable<WordInfo>? words;

    public TagCloudConfigurationForm(IVisualizationProvider visualizationProvider,
        IEnumerable<IColorPicker> colorPickers,
        IEnumerable<ILayoutProvider> layoutProviders)
    {
        Font = new Font("Arial", 22, FontStyle.Regular, GraphicsUnit.Pixel);
        InitializeComponent();
        var table = new TableLayoutPanel { Dock = DockStyle.Fill };
        table.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        table.RowStyles.Add(new RowStyle(SizeType.Absolute, 662));
        table.RowStyles.Add(new RowStyle(SizeType.Absolute, 20));
        table.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));

        table.Controls.Add(
            new SettingsTable(visualizationProvider, colorPickers, layoutProviders, this) { Dock = DockStyle.Fill }, 0,
            0);
        table.Controls.Add(new Panel { Dock = DockStyle.Fill }, 0, 1);
        table.Controls.Add(new PushButtonPanel(visualizationProvider, this), 0, 2);

        Controls.Add(table);
    }

    public IEnumerable<WordInfo>? Words
    {
        get => words;
        set
        {
            words = value;
            TextIsUploaded?.Invoke();
            DataHasBeenUpdated?.Invoke(CheckCorrectnessOfData());
        }
    }

    public IColorPicker? ColorPicker
    {
        get => colorPicker;
        set
        {
            colorPicker = value;
            DataHasBeenUpdated?.Invoke(CheckCorrectnessOfData());
        }
    }

    public ILayoutProvider? LayoutProvider
    {
        get => layoutProvider;
        set
        {
            layoutProvider = value;
            DataHasBeenUpdated?.Invoke(CheckCorrectnessOfData());
        }
    }

    public bool EverythingIsPrepared
    {
        get => everythingIsPrepared;
        set
        {
            everythingIsPrepared = value;
            if (value) SetupIsFinished?.Invoke();
        }
    }

    public event Action? SetupIsFinished;
    public event Action<bool>? DataHasBeenUpdated;
    public event Action? TextIsUploaded;

    private bool CheckCorrectnessOfData()
    {
        return Words is not null && ColorPicker is not null && LayoutProvider is not null;
    }
}