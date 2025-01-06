namespace TagCloud.ReadingFiles;

public class ReaderPicker : IReaderProvider
{
    private readonly Dictionary<string, IReader> readers = new();
    
    public ReaderPicker(IEnumerable<IReader> readers)
    {
        foreach (var reader in readers)
            foreach (var extension in reader.AvailableExtensions)
                this.readers[extension] = reader;
    }

    public IEnumerable<string> GetSupportedExtensions()
    {
        return readers.Keys;
    }

    public Func<IEnumerable<string>> GetReader(string pathToFile)
    {
        if (!Path.Exists(pathToFile))
            throw new FileNotFoundException($"Файл {pathToFile} не существует или поврежден");
        var fileExtension = Path.GetExtension(pathToFile);
        if (!readers.TryGetValue(fileExtension, out var reader))
            throw new Exception($"Не найден подходящий {nameof(IReader)} для файла с расширением {fileExtension}");
        return () => reader.ReadTextLineByLine(pathToFile);
    }
}