using System.ComponentModel.DataAnnotations;

namespace PharmaControl.Application.DTO.Employee;

public class EmployeeRequestDto
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [MaxLength(200)]
    public string Name { get; set; } = null!;
    
    public DateTime BirthDate { get; set; } = DateTime.UtcNow;
    public DateTime HiringDate { get; set; } = DateTime.UtcNow;
    [Required]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve ter 11 caracteres.")]
    public string CPF { get; set; } = null!;

    public string Role { get; set; } = default!;
    [Phone]
    public string? Phone { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
}