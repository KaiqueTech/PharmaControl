namespace PharmaControl.Application.DTO.Employee;

public class EmployeeResponseDto
{
    public int IdEmployee { get; set; }
    public string Name { get; set; } = null!;
    public string CPF { get; set; } = null!;
    public string Role { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public DateTime HiringDate { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string Status { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    
    public void SetUpdatedAt(DateTime updatedAt) => UpdatedAt = updatedAt;
    public void SetCreatedAt(DateTime createdAt) => CreatedAt = createdAt;
}