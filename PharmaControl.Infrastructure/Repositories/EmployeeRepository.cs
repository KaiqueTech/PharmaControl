using Microsoft.EntityFrameworkCore;
using PharmaControl.Application.Interfaces;
using PharmaControl.Common.Enuns;
using PharmaControl.Domain.Models;
using PharmaControl.Infrastructure.DataContext;

namespace PharmaControl.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    
    private readonly PharmaDbContext _context;

    public EmployeeRepository(PharmaDbContext context)
    {
        _context = context;
    }
    public async Task<EmployeeModel> AddEmployeeAsync(EmployeeModel employee)
    {
        await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync();
        return employee;
    }

    public async Task<EmployeeModel> GetEmployeeByIdAsync(int id)
    {
        try
        {
            var result = await _context.Employees.FirstOrDefaultAsync(e => e.IdEmployee == id);
            if (result is null)
            {
                throw new KeyNotFoundException($"Employee with ID {id} not found.");
            }
            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        
    }

    public async Task<EmployeeModel> GetByCpfAsync(string cpf)
    {
        var result =  await _context.Employees
            .FirstOrDefaultAsync(e => e.CPF == cpf);
        if (result is null)
        {
            throw new KeyNotFoundException($"Employee with CPF: {cpf} not found.");
        }
        return result;
    }

    public async Task<List<EmployeeModel>> GetByRoleAsync(string role)
    {
        if (string.IsNullOrWhiteSpace(role))
            return new List<EmployeeModel>();

        if (!Enum.TryParse<RoleEnum>(role, true, out var parsedRole))
            return new List<EmployeeModel>();

        return await _context.Employees
            .Where(e => e.Role == parsedRole)
            .ToListAsync();
    }

    public async Task<IEnumerable<EmployeeModel>> GetAllEmployeesAsync()
    {
        var result = await _context.Employees.ToListAsync();
        return result;
    }

    public async Task<EmployeeModel> UpdateEmployeeAsync(int id, EmployeeModel employee)
    {
        var existingEmployee = await _context.Employees.FirstOrDefaultAsync(e => e.IdEmployee == id);

        if (existingEmployee is null)
            throw new KeyNotFoundException($"Employee with ID {id} not found.");
        
        existingEmployee.UpdateContact(employee.Phone, employee.Email);
        existingEmployee.ChangeRole(employee.Role);
        existingEmployee.UpdateName(employee.Name);
        existingEmployee.UpdateBirthDate(employee.BirthDate);
        existingEmployee.UpdateHiringDate(employee.HiringDate);

        existingEmployee.SetUpdatedAt(DateTime.UtcNow);

        _context.Employees.Update(existingEmployee);
        await _context.SaveChangesAsync();

        return existingEmployee;
    }

    public async Task ToggleStatusAsync(int id, bool isActive)
    {
        var existingEmployee = await _context.Employees.FirstOrDefaultAsync(e => e.IdEmployee == id);

        if (existingEmployee is null)
            throw new KeyNotFoundException($"Employee with ID {id} not found.");

        existingEmployee.SetIsActive(isActive);
        await _context.SaveChangesAsync();
    }
}