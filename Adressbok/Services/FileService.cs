using Adressbok.Interfaces;
using System.Text.Json;

namespace Adressbok.Services;

internal class FileService<T> : IFileService<T> where T : class
{
    private readonly string _currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
    private readonly string _filePath;

    public FileService(string fileName)
    {
        _filePath = Path.Combine(_currentDirectory, $"{fileName}.json");
    }

    public IEnumerable<T> ReadFromFile()
    {
        if (File.Exists(_filePath))
        {
            using var sw = new StreamReader(_filePath);
            var content = sw.ReadToEnd();
            if (!string.IsNullOrEmpty(content))
                return JsonSerializer.Deserialize<IEnumerable<T>>(content)!;
        }

        return Enumerable.Empty<T>();
    }

    public void WriteToFile(IEnumerable<T> items)
    {
        if (items is null) return;

        var json = JsonSerializer.Serialize(items);

        using var sw = new StreamWriter(_filePath);
        sw.WriteLine(json);
    }
}
