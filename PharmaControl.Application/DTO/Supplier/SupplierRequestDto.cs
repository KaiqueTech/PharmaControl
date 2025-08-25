using System.ComponentModel.DataAnnotations;

namespace PharmaControl.Application.DTO.Supplier;

public class SupplierRequestDto
{
    [Required(ErrorMessage = "A razão social é obrigatório.")]
    [MaxLength(200)]
    public string SocialReason { get; set; } = null!;
    public string FantasyName { get; set; } = null!;

    [Required]
    [StringLength(14, MinimumLength = 14, ErrorMessage = "O CNPJ deve ter 14 caracteres.")]
    public required string Cnpj { get; set; }
    public string StateRegistration { get; set; } = null!;
    public string? Address { get; set; }

    [Phone]
    public string? Phone { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
}