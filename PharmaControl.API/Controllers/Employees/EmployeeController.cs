using Microsoft.AspNetCore.Mvc;
using PharmaControl.Application.DTO.Employee;
using PharmaControl.Application.DTO.Shared;
using PharmaControl.Application.Interfaces.Employee;

namespace PharmaControl.API.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }
    
    [HttpPost]
    [Tags("CreateEmployee")]
    public async Task<ActionResult<ResultDto<EmployeeResponseDto>>> CreateEmployee([FromBody]EmployeeRequestDto requestDto)
    {
        try
        {
            var result = await _employeeService.AddEmployeeAsync(requestDto);
            
            return Ok(result); 
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpGet]
    [Tags("GetAllEmployee")]
    public async Task<ActionResult<ResultDto<EmployeeResponseDto>>> GetAllEmployee([FromQuery]int pageNumber = 1, [FromQuery]int pageSize = 10)
    {
        try
        {
            var result = await _employeeService.GetAllEmployeesAsync(pageNumber, pageSize);
            return !result.Success ? NotFound() : Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocurred a error: {ex.Message}");
        }
    }

    [HttpGet("by-id/{id}")]
    [Tags("GetById")]
    public async Task<ActionResult> GetEmployeeById(int id)
    {
        try
        {
            var result = await _employeeService.GetEmployeeByIdAsync(id);
            return !result.Success ? NotFound() : Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpGet("by-cpf/{cpf}")]
    [Tags("GetByCpf")]
    public async Task<ActionResult> GetEmployeeByCpf(string cpf)
    {
        try
        {
            var result = await _employeeService.GetEmployeeByCpfAsync(cpf);
            return !result.Success ? NotFound() : Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpGet("by-role/{role}")]
    [Tags("GetByRole")]
    public async Task<ActionResult> GetEmployeeByRole(string role)
    {
        try
        {
            var result = await _employeeService.GetEmployeesByRoleAsync(role);
            return !result.Success ? NotFound() : Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new {message = ex.Message });
        }
    }

    [HttpPut("by-update-employee/{id}")]
    [Tags("UpdateEmployee")]
    public async Task<ActionResult<ResultDto<EmployeeResponseDto>>> UpdateEmployee(int id,
        [FromBody]EmployeeRequestDto employeeRquestDto)
    {
        try
        {
            var result = await _employeeService.UpdateEmployeeAsync(id, employeeRquestDto);
            
            return Ok(result); 
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpPut("by-activate-desactvate/{id}")]
    [Tags("UpdateEmployee")]
    public async Task<ActionResult<ResultDto<EmployeeResponseDto>>> UpdateStatus(int id)
    {
        try
        {
            var result = await _employeeService.ToggleStatusAsync(id);
            
            return Ok(result); 
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }
}