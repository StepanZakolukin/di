using TagCloud.ImageGeneration;
using TagCloud.TextProcessing;

namespace TagCloudGUI.Controls;

public sealed class PushButtonPanel : TableLayoutPanel
{
    private Bitmap image;
    
    private readonly MyButton textUploadMyButton = new()
    {
        Text = "Загрузить текст",
        Width = 230,
    };

    private readonly MyButton cloudGenerationButton = new()
    {
        Text = "Сгенерировать",
        Width = 220,
    };

    private readonly MyButton imageSaveButton = new()
    {
        Text = "Сохранить",
        Width = 174,
    };
    
    private readonly IVisualizationProvider visualizationProvider;
    private readonly TagCloudConfigurationForm ParentForm;
    
    public PushButtonPanel(IVisualizationProvider visualizationProvider, TagCloudConfigurationForm parentForm)
    {
        Dock = DockStyle.Fill;
        ParentForm = parentForm;
        this.visualizationProvider = visualizationProvider;
        ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 230));
        ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 174));
        ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 220));
        ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 116));
        ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180));
        
        Controls.Add(textUploadMyButton, 0, 0);
        Controls.Add(new Panel {Dock = DockStyle.Fill}, 1, 0);
        Controls.Add(cloudGenerationButton, 2, 0);
        Controls.Add(new Panel { Dock = DockStyle.Fill }, 3, 0);
        Controls.Add(imageSaveButton, 4, 0);

        imageSaveButton.Enabled = false;
        cloudGenerationButton.Enabled = false;
        textUploadMyButton.Click += SelectFile;
        cloudGenerationButton.Click += GenerateImage;
        imageSaveButton.Click += SaveImage;
        ParentForm.DataHasBeenUpdated += correct => cloudGenerationButton.Enabled = correct;
    }
    
    private void SelectFile(object? sender, EventArgs e)
    {
        var openFileDialog = new OpenFileDialog
        {
            Filter = "txt files (*.txt)|*.txt",
            RestoreDirectory = true
        };

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            var filePath = openFileDialog.FileName;
            ParentForm.Words = new TextPreprocessing().PerformPreprocessing(filePath);
        }
        
        imageSaveButton.Enabled = false;
    }

    private void GenerateImage(object? sender, EventArgs e)
    {
        ParentForm.EverythingIsPrepared = true;
        image = visualizationProvider.CreateImage(ParentForm.FilterWords, ParentForm.ColorPicker, ParentForm.LayoutProvider);
        imageSaveButton.Enabled = true;
        ParentForm.LayoutProvider = ParentForm.LayoutProvider.ResetLayout();
    }

    private void SaveImage(object? sender, EventArgs e)
    {
        var saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "Изображение (*.png)|*.png";
        saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        saveFileDialog.Title = "Сохранение файла";

        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            var filePath = saveFileDialog.FileName;
            image.Save(filePath);
        }
        
        imageSaveButton.Enabled = false;
    }
}