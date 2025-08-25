using PharmaControl.Application.DTO.Supplier;
using PharmaControl.Application.DTO.Shared;

namespace PharmaControl.Application.Interfaces.Supplier;

public interface ISupplierService
{
    public Task<PagedResultDto<SupplierResponseDto>> GetAllSuppliersAsync(int pageNumber, int pageSize);
    public Task<ResultDto<SupplierResponseDto>> GetSupplierByIdAsync(int id);
    public Task<ResultDto<SupplierResponseDto>> GetSupplierByCnpjAsync(string cnpj);
    public Task<ResultDto<SupplierResponseDto>>AddSupplierAsync(SupplierRequestDto supplierRquestDto);
    public Task<ResultDto<SupplierResponseDto>> UpdateSupplierAsync(int id, SupplierRequestDto supplierRquestDto);
    public Task<ResultDto<SupplierResponseDto>> ToggleStatusAsync(int id);
}