using TagCloud.TextProcessing;

namespace TagCloudGUI;

public partial class TagCloudConfigurationForm : Form
{
    private IEnumerable<WordInfo> Words;
    public TagCloudConfigurationForm()
    {
        InitializeComponent();
        MaximizeBox = false;
        Size = new Size(1000, 800);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        Padding = new Padding(40, 35, 40, 35);
    }

    private void Method()
    {
        var label = new Label
        {
            Location = new Point(300, 50),
            Size = new Size(200, 50),
            BackColor = Color.White,
        };
        Controls.Add(label);
        var fileUploadButton = new Button
        {
            Location = new Point(10, 10),
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
            Words = textHandler.PerformPreprocessing(filePath);
        }
    }
}