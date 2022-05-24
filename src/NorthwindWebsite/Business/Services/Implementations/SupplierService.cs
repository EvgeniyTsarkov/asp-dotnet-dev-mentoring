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

    public async Task<List<Supplier>> GetAll() =>
        await _supplierRepository.GetAll();

    public async Task<Dictionary<int, string>> GetSupplierOptions()
    {
        var suppliers = await _supplierRepository.GetAll();

        var supplierOptions = new Dictionary<int, string>();

        suppliers.ForEach(s => supplierOptions.Add(s.SupplierId, s.CompanyName));

        return supplierOptions;
    }
}
