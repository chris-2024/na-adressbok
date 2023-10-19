namespace Adressbok.Interfaces;

/// <summary>
/// Defines a generic file service interface for reading and writing collections of objects of type T to and from a file.
/// </summary>
/// <typeparam name="T">The type of objects to be read from and written to the file.</typeparam>
public interface IFileService<T> where T : class
{
    /// <summary>
    /// Reads a collection of objects from a file and returns them as an enumerable.
    /// </summary>
    /// <returns>An enumerable collection of objects read from the file.</returns>
    IEnumerable<T> ReadFromFile();

    /// <summary>
    /// Writes a collection of objects to a file.
    /// </summary>
    /// <param name="items">The collection of objects to be written to the file.</param>
    void WriteToFile(IEnumerable<T> items);
}
