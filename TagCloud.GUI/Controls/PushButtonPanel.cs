namespace TagCloudGUI.Controls;

public sealed class PushButtonPanel : TableLayoutPanel
{
    private readonly MyButton textUploadMyButton = new()
    {
        Text = "Загрузить текст",
        Width = 230,
    };

    private readonly MyButton cloudGenerationMyButton = new()
    {
        Text = "Сгенерировать",
        Width = 220,
    };

    private readonly MyButton imageSaveMyButton = new()
    {
        Text = "Сохранить",
        Width = 174,
    };
    
    public PushButtonPanel()
    {
        Dock = DockStyle.Fill;
        ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 230));
        ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 167));
        ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 220));
        ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 123));
        ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180));
        
        Controls.Add(textUploadMyButton, 0, 0);
        Controls.Add(new Panel {Dock = DockStyle.Fill}, 1, 0);
        Controls.Add(cloudGenerationMyButton, 2, 0);
        Controls.Add(new Panel { Dock = DockStyle.Fill }, 3, 0);
        Controls.Add(imageSaveMyButton, 4, 0);
    }
}