namespace Adressbok.Interfaces;

public interface IFileService<T> where T : class
{
    IEnumerable<T> ReadFromFile();
    bool WriteToFile(IEnumerable<T> items);
}
