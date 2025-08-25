using AutoMapper;
using PharmaControl.Application.DTO.Employee;
using PharmaControl.Application.DTO.Shared;
using PharmaControl.Application.Interfaces;
using PharmaControl.Application.Interfaces.Employee;
using PharmaControl.Common.Enuns;
using PharmaControl.Domain.Models;

namespace PharmaControl.Application.Services.Employee;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }
    
    public async Task<ResultDto<EmployeeResponseDto>> AddEmployeeAsync(EmployeeRequestDto employeeRequestDto)
    {
        try
        {
            var existingEmployee = await _employeeRepository.GetByCpfAsync(employeeRequestDto.CPF);
            if (existingEmployee is not null)
                throw new InvalidOperationException($"Já existe um funcionário com o CPF {employeeRequestDto.CPF}.");

            var employee = _mapper.Map<EmployeeModel>(employeeRequestDto);
            var createdEmployee = await _employeeRepository.AddEmployeeAsync(employee);

            var response = _mapper.Map<EmployeeResponseDto>(createdEmployee);
        
            return ResultDto<EmployeeResponseDto>.Ok(response, $"Funcionário cadastrado com sucesso!");
        }
        catch (Exception ex)
        {
            return ResultDto<EmployeeResponseDto>.Fail($"Ocorreu um erro ao cadastrar o funcionario: {ex.Message}");
        }
    }
    
    public async Task<PagedResultDto<EmployeeResponseDto>> GetAllEmployeesAsync(int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            var query = await _employeeRepository.GetAllEmployeesAsync();

            var totalRecords = query.Count();

            var employees =  query
                .OrderByDescending(e => e.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            
            var listEmployees = _mapper.Map<List<EmployeeResponseDto>>(employees);
            
            return PagedResultDto<EmployeeResponseDto>.Ok(
                listEmployees,
                pageNumber,
                pageSize,
                totalRecords,
                "Employees listing sucessfully"
                );
        }
        catch (Exception e)
        {
            return PagedResultDto<EmployeeResponseDto>.Fail($"Error fetching employees: {e.Message}");
        }
    }

    public async Task<ResultDto<EmployeeResponseDto>> GetEmployeeByIdAsync(int id)
    {
        try
        {
            var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(id);
       
            var responseEmployee = _mapper.Map<EmployeeResponseDto>(existingEmployee);
            return ResultDto<EmployeeResponseDto>.Ok(responseEmployee, "Employee retrieved successfully");
        }
        catch (Exception ex)
        {
            return ResultDto<EmployeeResponseDto>.Fail($"Occurred an error: {ex.Message}");
        }
        
    }

    public async Task<ResultDto<EmployeeResponseDto>> GetEmployeeByCpfAsync(string cpf)
    {
        try
        {
            var existingEmployee = await _employeeRepository.GetByCpfAsync(cpf);
       
            var responseEmployee = _mapper.Map<EmployeeResponseDto>(existingEmployee);
            return ResultDto<EmployeeResponseDto>.Ok(responseEmployee, "Employee retrieved successfully");
        }
        catch (Exception ex)
        {
            return ResultDto<EmployeeResponseDto>.Fail($"Occurred an error: {ex.Message}");
        }
    }

    public async Task<ResultDto<List<EmployeeResponseDto>>> GetEmployeesByRoleAsync(string role)
    {
        
        try
        {
            var employees = await _employeeRepository.GetByRoleAsync(role);

            if (!employees.Any())
            {
                return ResultDto<List<EmployeeResponseDto>>.Fail($"No employees found for role: {role}");
            }

            var responseEmployees = _mapper.Map<List<EmployeeResponseDto>>(employees);

            return ResultDto<List<EmployeeResponseDto>>.Ok(responseEmployees, "Employees retrieved successfully");
        }
        catch (Exception ex)
        {
            return ResultDto<List<EmployeeResponseDto>>.Fail($"An error occurred: {ex.Message}");
        }
    }

    public async Task<ResultDto<EmployeeResponseDto>> UpdateEmployeeAsync(int id, EmployeeRequestDto employeeRequestDto)
    {
        try
        {
            var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(id);

            // if (existingEmployee == null)
            // {
            //     return ResultDto<EmployeeResponseDto>.Fail("Employee not found.");
            // }

            _mapper.Map(employeeRequestDto, existingEmployee);

            await _employeeRepository.UpdateEmployeeAsync(id,existingEmployee);

            var responseEmployee = _mapper.Map<EmployeeResponseDto>(existingEmployee);

            return ResultDto<EmployeeResponseDto>.Ok(responseEmployee, "Employee updated successfully");
        }
        catch (Exception ex)
        {
            return ResultDto<EmployeeResponseDto>.Fail($"An error occurred: {ex.Message}");
        }
    }

    public async Task<ResultDto<EmployeeResponseDto>> ToggleStatusAsync(int id)
    {
        try
        {
            var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (existingEmployee == null)
                return ResultDto<EmployeeResponseDto>.Fail("Employee not found.");
            
            var newStatus = existingEmployee.Status == StatusEnum.Ativo
                ? StatusEnum.Inativo
                : StatusEnum.Ativo;

            existingEmployee.SetIsActive(newStatus);

            await _employeeRepository.UpdateEmployeeAsync(id, existingEmployee);

            var responseEmployee = _mapper.Map<EmployeeResponseDto>(existingEmployee);

            return ResultDto<EmployeeResponseDto>.Ok(responseEmployee, "Employee updated successfully");
        }
        catch (Exception e)
        {
            return ResultDto<EmployeeResponseDto>.Fail($"Error updating employee: {e.Message}");
        }
    }

}