using Microsoft.EntityFrameworkCore;
using PharmaControl.Application.Interfaces;
using PharmaControl.Common.Enuns;
using PharmaControl.Domain.Models;
using PharmaControl.Infrastructure.DataContext;

namespace PharmaControl.Infrastructure.Repositories.Supplier;

public class SupplierRepository : ISupplierRepository
{
    
    private readonly PharmaDbContext _context;
    

    public SupplierRepository(PharmaDbContext context)
    {
        _context = context;
    }
    public async Task<SupplierModel> AddSupplierAsync(SupplierModel supplier)
    {
        try
        {
            await _context.Suppliers.AddAsync(supplier);
            await _context.SaveChangesAsync();
            return supplier;
        }
        catch (Exception ex)
        {
            throw new KeyNotFoundException($"Ocorreu um erro: {ex.Message}.");
        }
        
    }

    public async Task<SupplierModel> GetSupplierByIdAsync(int id)
    {
        try
        {
            var result = await _context.Suppliers.FirstOrDefaultAsync(e => e.IdSupplier == id);
            if (result is null)
            {
                throw new KeyNotFoundException($"Supplier with ID {id} not found.");
            }
            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        
    }

    public async Task<SupplierModel> GetByCnpjAsync(string cnpj)
    {
         var result = await _context.Suppliers
            .FirstOrDefaultAsync(e => e.Cnpj == cnpj);
        return result;
    }
    
    public async Task<SupplierModel> GetByCnpjOrThrowAsync(string cnpj)
    {
        var supplier = await GetByCnpjAsync(cnpj);
        if (supplier == null)
            throw new KeyNotFoundException($"Supplier with CNPJ: {cnpj} not found.");
        return supplier;
    }
    
    public async Task<IEnumerable<SupplierModel>> GetAllSupplierAsync()
    {
        var result = await _context.Suppliers.ToListAsync();
        return result;
    }

    public async Task<SupplierModel> UpdateSupplierAsync(int id, SupplierModel supplier)
    {
        var existingSupplier = await _context.Suppliers.FirstOrDefaultAsync(e => e.IdSupplier == id);

        if (existingSupplier is null)
            throw new KeyNotFoundException($"Supplier with ID {id} not found.");
        
        existingSupplier.UpdateContact(supplier.Phone, supplier.Email);
        existingSupplier.UpdateAddress(supplier.Address);

        existingSupplier.SetUpdatedAt(DateTime.UtcNow);

        _context.Suppliers.Update(existingSupplier);
        await _context.SaveChangesAsync();

        return existingSupplier;
    }

    public async Task ToggleStatusAsync(int id, bool isActive)
    {
        var existingEmployee = await _context.Suppliers
            .FirstOrDefaultAsync(e => e.IdSupplier == id);

        if (existingEmployee is null)
            throw new KeyNotFoundException($"Supplier with ID {id} not found.");

        existingEmployee.SetIsActive(
            existingEmployee.Status == StatusEnum.Ativo 
                ? StatusEnum.Inativo 
                : StatusEnum.Ativo
        );

        await _context.SaveChangesAsync();
    }
}