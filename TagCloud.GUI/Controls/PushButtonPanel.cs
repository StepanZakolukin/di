using TagCloud.ImageGeneration;

namespace TagCloudGUI.Controls;

public sealed class PushButtonPanel : TableLayoutPanel
{
    private readonly TagCloudButton cloudGenerationButton = new()
    {
        Text = "Сгенерировать",
        Width = 220
    };

    private readonly TagCloudConfigurationForm parentForm;

    private readonly TagCloudButton textUploadTagCloudButton = new()
    {
        Text = "Загрузить текст",
        Width = 230
    };

    private readonly IVisualizationProvider visualizationProvider;

    public PushButtonPanel(IVisualizationProvider visualizationProvider, TagCloudConfigurationForm parentForm)
    {
        Dock = DockStyle.Fill;
        this.parentForm = parentForm;
        this.visualizationProvider = visualizationProvider;
        ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 230));
        ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 220));

        Controls.Add(textUploadTagCloudButton, 0, 0);
        Controls.Add(new Panel { Dock = DockStyle.Fill }, 1, 0);
        Controls.Add(cloudGenerationButton, 2, 0);
        
        cloudGenerationButton.Enabled = false;
        textUploadTagCloudButton.Click += SelectFile;
        cloudGenerationButton.Click += GenerateImage;
        this.parentForm.DataHasBeenUpdated += correct => cloudGenerationButton.Enabled = correct;
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
            parentForm.Words = parentForm.WordsProvider.PerformPreprocessing(filePath);
        }
    }

    private void GenerateImage(object? sender, EventArgs e)
    {
        var filePath = GetPathToSave();
        if (filePath == null) return;
        
        parentForm.EverythingIsPrepared = true;
        visualizationProvider.UserInputProvider.Words = parentForm.FilterWords;
        
        var image = visualizationProvider.CreateImage();
        parentForm.LayoutProvider?.ResetLayout();
        
        image.Save(filePath);
    }

    private string? GetPathToSave()
    {
        var saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "Изображение (*.png)|*.png";
        saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        saveFileDialog.Title = "Сохранение файла";

        if (saveFileDialog.ShowDialog() == DialogResult.OK)
            return saveFileDialog.FileName;

        return null;
    }
}