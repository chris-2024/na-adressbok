namespace Adressbok.Interfaces;

public interface IFileService<T>
{
    IEnumerable<T> ReadFromFile();
    void WriteToFile(IEnumerable<T> items);
}
