namespace NorthwindWebsite.Middleware.Handlers.Interfaces;

public interface IImageCachingHandler
{
    bool IsContained(string imageIndex);

    int GetNumberOfFilesInCachingFolder();

    void CreateFolderIfDoesNotExists();

    byte[] GetImageFromCache(int index);

    void DumpImageCache();
}
