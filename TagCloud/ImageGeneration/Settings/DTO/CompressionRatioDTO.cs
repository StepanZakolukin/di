namespace TagCloud.ImageGeneration.Settings.DTO;

public class CompressionRatioDto : ICrrectnessChecker
{
    private const float MaxValue = 10f;
    private const float MinValue = 0.1f;
    public bool IsCorrect { get; private set; }
    public event Action<ICrrectnessChecker, string>? ValueChanged;
    
    private string _ratio;
    public string Ratio
    {
        get => _ratio;
        set
        {
            _ratio = value;
            if (float.TryParse(value, out var number))
            {
                IsCorrect = CheckCorrectness(number);
                ValueChanged?.Invoke(this, IsCorrect ? string.Empty : $"{MinValue} <= коэффициент <= {MaxValue}");
            }
            else
            {
                IsCorrect = false;
                ValueChanged?.Invoke(this, "Не является числом");
            }
        }
    }

    public CompressionRatioDto(float ratio)
    {
        if (!CheckCorrectness(ratio))
            throw new ArgumentException($"{MinValue} <= {nameof(ratio)} <= {MaxValue}");
        Ratio = $"{ratio}";
    }

    private bool CheckCorrectness(float coefficient)
    {
        return coefficient is >= MinValue - float.Epsilon and <= MaxValue + float.Epsilon;
    }

    public float GetValueOrThrow()
    {
        if (float.TryParse(_ratio, out var number))
            return number;
        throw new InvalidCastException();
    }
}