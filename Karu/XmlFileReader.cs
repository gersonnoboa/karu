namespace Karu;

public interface IFileReader
{
    string ReadFile(string filePath);
}

public class XmlFileReader: IFileReader
{
    public string ReadFile(string filePath)
    {
        if (!filePath.EndsWith(".xml"))
        {
            throw new KaruException("File is not of .xml extension");
        }
        
        
        return File.ReadAllText(filePath);
    }
}
