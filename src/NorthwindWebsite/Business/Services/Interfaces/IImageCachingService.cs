namespace NorthwindWebsite.Business.Services.Interfaces;

public interface IImageCachingService
{
    bool IsContained(string imageIndex);

    int GetNumberOfFilesInCachingFolder();

    void CreateFolderIfDoesNotExists();

    byte[] GetImageFromCache(int index);

    void DumpImageCache();
}
