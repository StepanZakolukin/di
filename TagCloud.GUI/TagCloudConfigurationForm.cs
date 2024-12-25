using TagCloud.CloudLayout;
using TagCloud.ImageGeneration;
using TagCloud.TextProcessing;
using TagCloudGUI.Controls;
using Button = System.Windows.Forms.Button;

namespace TagCloudGUI;

public partial class TagCloudConfigurationForm : Form
{
    public TagCloudConfigurationForm(IVisualizationProvider visualizationProvider, IEnumerable<IColorPicker> colorPickers,
        IEnumerable<ILayoutProvider> layoutProviders)
    {
        Font = new Font("Arial", 22, FontStyle.Regular, GraphicsUnit.Pixel);
        InitializeComponent();
        var table = new TableLayoutPanel { Dock = DockStyle.Fill };
        table.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        table.RowStyles.Add(new RowStyle(SizeType.Absolute, 662));
        table.RowStyles.Add(new RowStyle(SizeType.Absolute, 20));
        table.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
        
        table.Controls.Add(new SettingsTable(visualizationProvider, colorPickers, layoutProviders) { Dock = DockStyle.Fill }, 0, 0);
        table.Controls.Add(new Panel { Dock = DockStyle.Fill }, 0, 1);
        table.Controls.Add(new PushButtonPanel(), 0, 2);
        
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