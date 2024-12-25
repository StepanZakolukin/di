using Microsoft.Extensions.DependencyInjection;
using TagCloud.CloudLayout;
using TagCloud.ImageGeneration;
using TagCloud.TextProcessing;
using TagCloudGUI.Controls;

namespace TagCloudGUI;

public partial class TagCloudConfigurationForm : Form
{
    private static readonly Font font = new("Arial", 24, FontStyle.Regular, GraphicsUnit.Pixel);

    public TagCloudConfigurationForm(IVisualizationProvider visualizationProvider, IEnumerable<IColorPicker> colorPickers,
        IEnumerable<ILayoutProvider> layoutProviders)
    {
        Font = font;
        InitializeComponent();

        var table = new TableLayoutPanel { Dock = DockStyle.Fill };
        table.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        table.RowStyles.Add(new RowStyle(SizeType.Absolute, 920));
        table.RowStyles.Add(new RowStyle(SizeType.Absolute, 14));
        table.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
        
        table.Controls.Add(new SettingsTable(visualizationProvider, colorPickers, layoutProviders) { Dock = DockStyle.Fill });
        table.Controls.Add(new Panel { Dock = DockStyle.Fill });
        table.Controls.Add(new PushButtonPanel());
        
        Controls.Add(table);
    }
    
    private void Method()
    {
        var label = new Label
        {
            /*Location = new Point(300, 50),*/
            Size = new Size(200, 50),
            BackColor = Color.White,
        };
        Controls.Add(label);
        var fileUploadButton = new Button
        {
            /*Location = new Point(10, 10),*/
            Size = new Size(100, 50),
            Text = "Загрузить текст"
        };
        fileUploadButton.Click += SelectFile;
        Controls.Add(fileUploadButton);
    }

    private void SelectFile(object sender, EventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
    
        // Настройка диалога
        /*openFileDialog.Filter = "(*.txt)";*/
        /*openFileDialog.FilterIndex = 1;*/
        /*openFileDialog.RestoreDirectory = true;*/

        // Открытие диалога и получение результата
        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            var filePath = openFileDialog.FileName;
        
            // Здесь вы можете обработать выбранный файл
            var textHandler = new TextPreprocessing();
            /*Words = textHandler.PerformPreprocessing(filePath);*/
        }
    }
}