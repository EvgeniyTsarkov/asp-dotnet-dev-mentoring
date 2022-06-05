using NorthwindWebsite.Business.Services.Interfaces;
using NorthwindWebsite.Core.ApplicationSettings;
using NorthwindWebsite.Core.CustomExceptions.BusinessExceptions;

namespace NorthwindWebsite.Business.Services.Implementations;

public class ImageCachingService : IImageCachingService
{
    private readonly AppSettings _appSettings;

    private readonly string _cachingFolder;

    public ImageCachingService(AppSettings appSettings)
    {
        _appSettings = appSettings;
        _cachingFolder = string.Concat(
            AppDomain.CurrentDomain.BaseDirectory,
            _appSettings.CachingConfigs.CachingFolder);
    }

    public bool IsContained(string imageIndex)
    {
        var imageName = string.Concat(_cachingFolder, imageIndex, ".bmp");

        var cachedFilesNames = Directory.GetFiles(_cachingFolder);

        return cachedFilesNames.Contains(imageName);
    }

    public int GetNumberOfFilesInCachingFolder() =>
        Directory.GetFiles(_cachingFolder).Length;

    public void CreateFolderIfDoesNotExists()
    {
        var directoryExists = Directory.Exists(_cachingFolder);

        if (!directoryExists)
        {
            Directory.CreateDirectory(_cachingFolder);
        }

        var existsAfterCreation = Directory.Exists(_cachingFolder);

        if (!existsAfterCreation)
        {
            throw new FolderNotCreatedException("Failed to create directory.");
        }
    }

    public byte[] GetImageFromCache(int index)
    {
        var filePath = string.Concat(_cachingFolder, index, ".bmp");

        var fileInfo = new FileInfo(filePath);

        byte[] byteArray = new byte[fileInfo.Length];

        using var fileStream = fileInfo.OpenRead();

        fileStream.Read(byteArray, 0, byteArray.Length);

        return byteArray;
    }

    public void DumpImageCache()
    {
        var directoryInfo = new DirectoryInfo(_cachingFolder);

        foreach (var file in directoryInfo.GetFiles())
        {
            file.Delete();
        }
    }
}
