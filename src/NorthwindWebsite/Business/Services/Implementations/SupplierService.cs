using Microsoft.AspNetCore.Mvc.Rendering;
using NorthwindWebsite.Business.Services.Interfaces;
using NorthwindWebsite.Infrastructure.Repositories.Interfaces;

namespace NorthwindWebsite.Business.Services.Implementations;

public class SupplierService : ISupplierService
{
    private readonly ISupplierRepository _supplierRepository;

    public SupplierService(ISupplierRepository supplierRepository)
    {
        _supplierRepository = supplierRepository;
    }

    public async Task<SelectList> GetSupplerSelectList()
    {
        var suppliers = await _supplierRepository.GetAll();

        return new SelectList(suppliers, "SupplierId", "CompanyName");
    }
}
