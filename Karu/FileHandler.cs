using System.Net;

namespace Karu;

public class FileHandler
{
    public static void CopyFiles(MusicLibraryInformation musicLibraryInformation, string targetDirectory)
    {
        if (!File.Exists(targetDirectory))
        {
            Directory.CreateDirectory(targetDirectory);
        }

        var numberOfFiles = musicLibraryInformation.Tracks.Count;

        foreach (var (file, index) in musicLibraryInformation.Tracks.WithIndex())
        {
            var relativePath = file[musicLibraryInformation.MusicFolder.Length..];
            var targetPath = FormattedFileName(Path.Combine(targetDirectory, relativePath));
            var directoryName = Path.GetDirectoryName(targetPath) ?? throw new KaruException("Invalid file");
            var targetDir = FormattedFileName(directoryName);

            if (targetDir != null && !Directory.Exists(targetDir))
            {
                Directory.CreateDirectory(targetDir);
            }

            var oldFile = FormattedFileName(file);

            if (!File.Exists(oldFile))
            {
                throw new KaruException("File doesn't exist, " + oldFile);
            }

            Console.WriteLine($"Copying file {index + 1} of {numberOfFiles}: {relativePath}");
            File.Copy(oldFile, targetPath, overwrite: true);
        }
    }

    private static string FormattedFileName(string unformattedFileName)
    {
        var replacedString = unformattedFileName.Replace("file://", "");
        var decodedString = Uri.UnescapeDataString(replacedString);

        return decodedString;
    }
}
