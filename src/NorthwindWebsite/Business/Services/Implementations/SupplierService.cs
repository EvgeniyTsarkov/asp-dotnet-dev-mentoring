using Microsoft.AspNetCore.Mvc.Rendering;
using NorthwindWebsite.Business.Services.Interfaces;
using NorthwindWebsite.Entities;
using NorthwindWebsite.Infrastructure.Repositories.Interfaces;

namespace NorthwindWebsite.Business.Services.Implementations;

public class SupplierService : ISupplierService
{
    private readonly IGenericRepository<Supplier> _supplierRepository;

    public SupplierService(IGenericRepository<Supplier> supplierRepository)
    {
        _supplierRepository = supplierRepository;
    }

    public async Task<SelectList> GetSupplerSelectList()
    {
        var suppliers = await _supplierRepository.GetAll();

        return new SelectList(suppliers, "SupplierId", "CompanyName");
    }
}
