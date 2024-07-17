using System.Xml;

namespace Karu;

public class XmlParser
{
    public MusicLibraryInformation ParseXmlToTrackList(string xml, string mediaLocalizedReplacement)
    {
        var doc = new XmlDocument();
        doc.LoadXml(xml);

        if (doc == null)
        {
            throw new KaruException("Invalid XML.");
        }

        var result = new List<string>();
        XmlNode? tracksNode = doc.SelectSingleNode("//dict[key='Tracks']/dict") ?? throw new KaruException("Invalid XML, can't retrieve tracks.");
        XmlNodeList trackNodes = tracksNode.ChildNodes;

        for (int i = 0; i < trackNodes.Count; i += 2)
        {
            XmlNode dictNode = trackNodes[i + 1];

            for (int j = 0; j < dictNode.ChildNodes.Count; j += 2)
            {
                string key = dictNode.ChildNodes[j].InnerText;

                if (key != "Location")
                {
                    continue;
                }

                XmlNode valueNode = dictNode.ChildNodes[j + 1];
                string path = valueNode.InnerText;
                result.Add(path);
            }
        }

        var musicFolder = ExtractMusicFolderInformation(doc);

        return new MusicLibraryInformation(result, musicFolder);
    }

    private string ExtractMusicFolderInformation(XmlDocument doc)
    {
        XmlNode? musicFolderNode = doc.SelectSingleNode("//key[text()='Music Folder']") ?? throw new KaruException("Invalid XML, can't retrieve music folder.");
        var musicFolderValue = musicFolderNode.NextSibling;

        if (musicFolderValue == null || musicFolderValue.Name != "string")
        {
            throw new KaruException("Invalid XML, can't retrieve music folder.");
        }

        return musicFolderValue.InnerText;
    }
}
