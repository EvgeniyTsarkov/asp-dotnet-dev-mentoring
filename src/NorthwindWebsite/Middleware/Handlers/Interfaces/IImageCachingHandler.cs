namespace NorthwindWebsite.Middleware.Handlers.Interfaces;

public interface IImageCachingHandler
{
    bool IsContained(string imageIndex);

    int GetNumberOfFilesInCachingFolder();

    void CreateCachingFolder();

    byte[] GetImageFromCache(int index);

    void DumpImageCache();

    bool DoesCachingDirectoryExist();
}
