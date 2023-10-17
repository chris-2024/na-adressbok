namespace Adressbok.Interfaces;

public interface IFileService<T> where T : class
{
    IEnumerable<T> ReadFromFile();
    void WriteToFile(IEnumerable<T> items);
}
