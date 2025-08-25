using PharmaControl.Domain.Models;

namespace PharmaControl.Application.Interfaces;

public interface ISupplierRepository
{
    Task<SupplierModel> AddSupplierAsync(SupplierModel supplier);
    Task<SupplierModel> GetSupplierByIdAsync(int id); 
    Task<SupplierModel> GetByCnpjAsync(string cnpj);
    Task<IEnumerable<SupplierModel>> GetAllSupplierAsync();
    Task<SupplierModel> UpdateSupplierAsync(int id, SupplierModel supplier);
    Task ToggleStatusAsync(int id, bool isActive);
}