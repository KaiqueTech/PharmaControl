using PharmaControl.Application.DTO.Shared;
using PharmaControl.Domain.Models;

namespace PharmaControl.Application.Interfaces;

public interface IEmployeeRepository
{
    Task<EmployeeModel> AddEmployeeAsync(EmployeeModel employee);
    Task<EmployeeModel> GetEmployeeByIdAsync(int id); 
    Task<EmployeeModel> GetByCpfAsync(string cpf);
    Task<List<EmployeeModel>> GetByRoleAsync(string role);
    Task<IEnumerable<EmployeeModel>> GetAllEmployeesAsync();
    Task<EmployeeModel> UpdateEmployeeAsync(int id, EmployeeModel employee);
    Task ToggleStatusAsync(int id);
}