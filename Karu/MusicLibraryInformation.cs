namespace Karu;

public class MusicLibraryInformation(List<string> tracks, string musicFolder)
{
    public List<string> Tracks { get; set; } = tracks;
    public string MusicFolder { get; set; } = musicFolder;
}