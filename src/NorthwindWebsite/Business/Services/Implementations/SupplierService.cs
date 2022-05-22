using Microsoft.AspNetCore.Mvc.Rendering;
using NorthwindWebsite.Business.Services.Interfaces;
using NorthwindWebsite.Infrastructure.Repositories.Interfaces;

namespace NorthwindWebsite.Business.Services.Implementations
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<List<SelectListItem>> GetSelectListItems()
        {
            var suppliers = await _supplierRepository.GetAll();

            return suppliers.Select(x =>
            new SelectListItem { Text = x.CompanyName, Value = x.SupplierId.ToString() })
                .ToList();
        }
    }
}
