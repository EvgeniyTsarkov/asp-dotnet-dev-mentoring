﻿using NorthwindWebsite.Business.Models;
using NorthwindWebsite.Infrastructure.Entities;

namespace NorthwindWebsite.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetAll();

    Task<Dictionary<int, string>> GetCategoryOptions();

    Task<FileUploadDto> GetFileUploadModel(int id);

    Task UpdateCategoryWithPicture(FileUploadDto fileUploadModel);

    Task<byte[]> GetImage(int id);
}
