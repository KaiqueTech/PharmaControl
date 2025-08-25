using AutoMapper;
using PharmaControl.Application.DTO.Supplier;
using PharmaControl.Application.DTO.Shared;
using PharmaControl.Application.Interfaces;
using PharmaControl.Application.Interfaces.Supplier;
using PharmaControl.Common.Enuns;
using PharmaControl.Domain.Models;

namespace PharmaControl.Application.Services.Supplier;

public class SupplierService : ISupplierService
{
    private readonly ISupplierRepository _supplierRepository;
    private readonly IMapper _mapper;

    public SupplierService(ISupplierRepository supplierRepository, IMapper mapper)
    {
        _supplierRepository = supplierRepository;
        _mapper = mapper;
    }
    
    public async Task<ResultDto<SupplierResponseDto>> AddSupplierAsync(SupplierRequestDto supplierRequestDto)
    {
        try
        {
            var existingSupplier = await _supplierRepository.GetByCnpjAsync(supplierRequestDto.Cnpj);
            if (existingSupplier is not null)
                return ResultDto<SupplierResponseDto>.Fail(
                    $"Já existe um fornecedor com o CNPJ {supplierRequestDto.Cnpj}.");

            var supplier = _mapper.Map<SupplierModel>(supplierRequestDto);
            var createdSupplier = await _supplierRepository.AddSupplierAsync(supplier);

            var response = _mapper.Map<SupplierResponseDto>(createdSupplier);
            return ResultDto<SupplierResponseDto>.Ok(response, "Fornecedor cadastrado com sucesso!");
        }
        catch (Exception ex)
        {
            return ResultDto<SupplierResponseDto>.Fail($"Ocorreu um erro ao cadastrar o fornecedor: {ex.Message}");
        }
    }
    
    public async Task<PagedResultDto<SupplierResponseDto>> GetAllSuppliersAsync(int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            var query = await _supplierRepository.GetAllSupplierAsync();

            var totalRecords = query.Count();

            var employees =  query
                .OrderByDescending(e => e.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            
            var listEmployees = _mapper.Map<List<SupplierResponseDto>>(employees);
            
            return PagedResultDto<SupplierResponseDto>.Ok(
                listEmployees,
                pageNumber,
                pageSize,
                totalRecords,
                "Suppliers listing sucessfully"
                );
        }
        catch (Exception e)
        {
            return PagedResultDto<SupplierResponseDto>.Fail($"Error fetching suppliers: {e.Message}");
        }
    }

    public async Task<ResultDto<SupplierResponseDto>> GetSupplierByIdAsync(int id)
    {
        try
        {
            var existingEmployee = await _supplierRepository.GetSupplierByIdAsync(id);
       
            var responseEmployee = _mapper.Map<SupplierResponseDto>(existingEmployee);
            return ResultDto<SupplierResponseDto>.Ok(responseEmployee, "Supplier retrieved successfully");
        }
        catch (Exception ex)
        {
            return ResultDto<SupplierResponseDto>.Fail($"Occurred an error: {ex.Message}");
        }
        
    }

    public async Task<ResultDto<SupplierResponseDto>> GetSupplierByCnpjAsync(string cnpj)
    {
        try
        {
            var supplier = await _supplierRepository.GetByCnpjAsync(cnpj);
            if (supplier == null)
                return ResultDto<SupplierResponseDto>.Fail($"Supplier with CNPJ {cnpj} not found.");

            var response = _mapper.Map<SupplierResponseDto>(supplier);
            return ResultDto<SupplierResponseDto>.Ok(response, "Supplier retrieved successfully");
        }
        catch (Exception ex)
        {
            return ResultDto<SupplierResponseDto>.Fail($"Occurred an error: {ex.Message}");
        }
    }
    

    public async Task<ResultDto<SupplierResponseDto>> UpdateSupplierAsync(int id, SupplierRequestDto supplierRequestDto)
    {
        try
        {
            var existingSupplier = await _supplierRepository.GetSupplierByIdAsync(id);
            
            _mapper.Map(supplierRequestDto, existingSupplier);

            await _supplierRepository.UpdateSupplierAsync(id,existingSupplier);

            var responseSupplier = _mapper.Map<SupplierResponseDto>(existingSupplier);

            return ResultDto<SupplierResponseDto>.Ok(responseSupplier, "Supplier updated successfully");
        }
        catch (Exception ex)
        {
            return ResultDto<SupplierResponseDto>.Fail($"An error occurred: {ex.Message}");
        }
    }

    public async Task<ResultDto<SupplierResponseDto>> ToggleStatusAsync(int id)
    {
        try
        {
            var existingSupplier = await _supplierRepository.GetSupplierByIdAsync(id);
            if (existingSupplier == null)
                return ResultDto<SupplierResponseDto>.Fail("Supplier not found.");
            
            var newStatus = existingSupplier.Status == StatusEnum.Ativo
                ? StatusEnum.Inativo
                : StatusEnum.Ativo;

            existingSupplier.SetIsActive(newStatus);

            await _supplierRepository.UpdateSupplierAsync(id, existingSupplier);

            var responseSupplier = _mapper.Map<SupplierResponseDto>(existingSupplier);

            return ResultDto<SupplierResponseDto>.Ok(responseSupplier, "Supplier updated successfully");
        }
        catch (Exception e)
        {
            return ResultDto<SupplierResponseDto>.Fail($"Error updating supplier: {e.Message}");
        }
    }

}