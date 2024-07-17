namespace Karu;

class Program
{
    static void Main(string[] args)
    {
        VerifyArguments(args);

        var xmlPath = args[0];

        var xmlFileReader = new XmlFileReader();
        var xml = xmlFileReader.ReadFile(xmlPath);

        var xmlParser = new XmlParser();
        var mediaLocalizedReplacement = args[1];
        var musicLibraryInformation = xmlParser.ParseXmlToTrackList(xml, mediaLocalizedReplacement);

        var destination = args[1];
        FileHandler.CopyFiles(musicLibraryInformation, destination);

        Console.WriteLine($"Success! {musicLibraryInformation.Tracks.Count} tracks copied");
        Console.WriteLine("Press any key to exit");
        Console.Read();
    }

    static void VerifyArguments(string[] args)
    {
        if (args.Length == 0)
        {
            throw new KaruException("Provide an XML path.");
        }

        if (args.Length == 1)
        {
            throw new KaruException("Provide a directory for saving the songs");
        }
    }
}
