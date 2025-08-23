using PharmaControl.Application.DTO.Employee;
using PharmaControl.Application.DTO.Shared;
using PharmaControl.Domain.Models;

namespace PharmaControl.Application.Interfaces.Employee;

public interface IEmployeeService
{
    public Task<PagedResultDto<EmployeeResponseDto>> GetAllEmployeesAsync(int pageNumber, int pageSize);
    public Task<ResultDto<EmployeeResponseDto>> GetEmployeeByIdAsync(int id);
    public Task<ResultDto<EmployeeResponseDto>> GetEmployeeByCpfAsync(string cpf);
    public Task<ResultDto<List<EmployeeResponseDto>>> GetEmployeesByRoleAsync(string role);

    public Task<ResultDto<EmployeeResponseDto>>AddEmployeeAsync(EmployeeRequestDto employeeRquestDto);
    public Task<ResultDto<EmployeeResponseDto>> UpdateEmployeeAsync(int id, EmployeeRequestDto employeeRquestDto);
    public Task<ResultDto<EmployeeResponseDto>> ToggleStatusAsync(int id);
}