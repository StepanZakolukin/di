namespace TagCloudGUI.Controls;

public class PushButtonPanel : TableLayoutPanel
{
    private Button textUploadButton = new()
    {
        Text = "Загрузить текст",
        Dock = DockStyle.Fill,
    };

    private Button cloudGenerationButton = new()
    {
        Text = "Сгенерировать",
        Dock = DockStyle.Fill,
    };

    private Button imageSaveButton = new()
    {
        Text = "Сохранить",
        Dock = DockStyle.Fill,
    };
    
    public PushButtonPanel()
    {
        Dock = DockStyle.Fill;
        RowStyles.Add(new RowStyle(SizeType.AutoSize));
        ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 230));
        ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 167));
        ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 220));
        ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 133));
        ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 170));
        
        Controls.Add(textUploadButton, 0, 0);
        Controls.Add(new Panel(), 1, 0);
        Controls.Add(cloudGenerationButton, 2, 0);
        Controls.Add(new Panel(), 3, 0);
        Controls.Add(imageSaveButton, 4, 0);
    }
}