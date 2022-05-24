using Microsoft.AspNetCore.Mvc.Rendering;
using NorthwindWebsite.Entities;

namespace NorthwindWebsite.Business.Services.Interfaces
{
    public interface ISupplierService
    {
        Task<List<Supplier>> GetAll();
    }
}
