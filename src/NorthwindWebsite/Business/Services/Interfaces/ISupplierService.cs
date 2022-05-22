using Microsoft.AspNetCore.Mvc.Rendering;

namespace NorthwindWebsite.Business.Services.Interfaces
{
    public interface ISupplierService
    {
        Task<List<SelectListItem>> GetSelectListItems();
    }
}
