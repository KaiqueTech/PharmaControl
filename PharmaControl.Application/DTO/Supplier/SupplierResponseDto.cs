namespace PharmaControl.Application.DTO.Supplier;

public class SupplierResponseDto
{
    public int IdSupplier { get; private set; }
        
    public string SocialReason { get; set; } = null!;
    public string FantasyName { get; set; } = null!;
    public string CNPJ { get; set; } = null!;
    public string StateRegistration { get; set; } = null!;
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string Status { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    
    public void SetUpdatedAt(DateTime updatedAt) => UpdatedAt = updatedAt;
    public void SetCreatedAt(DateTime createdAt) => CreatedAt = createdAt;
}