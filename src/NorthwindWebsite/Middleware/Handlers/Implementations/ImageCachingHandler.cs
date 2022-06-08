using NorthwindWebsite.Core.ApplicationSettings;
using NorthwindWebsite.Core.Constants;
using NorthwindWebsite.Core.CustomExceptions.BusinessExceptions;
using NorthwindWebsite.Middleware.Handlers.Interfaces;

namespace NorthwindWebsite.Middleware.Handlers.Implementations;

public class ImageCachingHandler : IImageCachingHandler
{
    private readonly AppSettings _appSettings;

    private readonly string _cachingFolder;

    public ImageCachingHandler(AppSettings appSettings)
    {
        _appSettings = appSettings;
        _cachingFolder = string.Concat(
            AppDomain.CurrentDomain.BaseDirectory,
            _appSettings.CachingConfigs.CachingFolder);
    }

    public bool IsContained(string imageIndex)
    {
        var imageName = string.Concat(_cachingFolder, imageIndex, FileNameConstants.BmpExtension);

        var cachedFilesNames = Directory.GetFiles(_cachingFolder);

        return cachedFilesNames.Contains(imageName);
    }

    public int GetNumberOfFilesInCachingFolder() =>
        Directory.GetFiles(_cachingFolder).Length;

    public void CreateFolderIfDoesNotExists()
    {
        if (!DoesCachingDirectoryExist())
        {
            Directory.CreateDirectory(_cachingFolder);
        }

        if (!DoesCachingDirectoryExist())
        {
            throw new FolderNotCreatedException("Failed to create directory.");
        }
    }

    public byte[] GetImageFromCache(int index)
    {
        var filePath = string.Concat(_cachingFolder, index, FileNameConstants.BmpExtension);

        var fileInfo = new FileInfo(filePath);

        var byteArray = new byte[fileInfo.Length];

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

    private bool DoesCachingDirectoryExist() => Directory.Exists(_cachingFolder);
}
