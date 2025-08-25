using Microsoft.AspNetCore.Mvc;
using PharmaControl.Application.DTO.Shared;
using PharmaControl.Application.DTO.Supplier;
using PharmaControl.Application.Interfaces.Supplier;

namespace PharmaControl.API.Controllers.Suppliers;

[ApiController]
[Route("api/supplier")]
public class SupplierController : ControllerBase
{
    private readonly ISupplierService _supplierService;

    public SupplierController(ISupplierService supplierService)
    {
        _supplierService = supplierService;
    }
    
    [HttpPost]
    [Tags("CreateSupplier")]
    public async Task<ActionResult<ResultDto<SupplierResponseDto>>> CreateSupplier([FromBody]SupplierRequestDto requestDto)
    {
        try
        {
            var result = await _supplierService.AddSupplierAsync(requestDto);
            
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
    [Tags("GetAllSuppliers")]
    public async Task<ActionResult<ResultDto<SupplierResponseDto>>> GetAllSupplier([FromQuery]int pageNumber = 1, [FromQuery]int pageSize = 10)
    {
        try
        {
            var result = await _supplierService.GetAllSuppliersAsync(pageNumber, pageSize);
            return !result.Success ? NotFound() : Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpGet("by-id/{id}")]
    [Tags("GetById")]
    public async Task<ActionResult<ResultDto<SupplierResponseDto>>> GetSupplierById(int id)
    {
        try
        {
            var result = await _supplierService.GetSupplierByIdAsync(id);
            return !result.Success ? NotFound() : Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpGet("by-cnpj/{cnpj}")]
    [Tags("GetByCnpj")]
    public async Task<ActionResult<ResultDto<SupplierResponseDto>>> GetSupplierByCnpj(string cnpj)
    {
        try
        {
            var result = await _supplierService.GetSupplierByCnpjAsync(cnpj);
            return !result.Success ? NotFound() : Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }
    

    [HttpPut("by-update-supplier/{id}")]
    [Tags("UpdateSupplier")]
    public async Task<ActionResult<ResultDto<SupplierResponseDto>>> UpdateSupplier(int id,
        [FromBody]SupplierRequestDto supplierRquestDto)
    {
        try
        {
            var result = await _supplierService.UpdateSupplierAsync(id, supplierRquestDto);
            
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

    [HttpPut("by-activate-desactivate/{id}")]
    [Tags("UpdateSupplier")]
    public async Task<ActionResult<ResultDto<SupplierResponseDto>>> UpdateStatus(int id)
    {
        try
        {
            var result = await _supplierService.ToggleStatusAsync(id);
            
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