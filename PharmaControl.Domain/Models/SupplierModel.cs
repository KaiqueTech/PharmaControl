namespace PharmaControl.Domain.Models
{
    public class SupplierModel
    {
        public int IdSupplier { get; private set; }
        
        public string SocialReason { get; private set; } = null!;
        public string FantasyName { get; private set; } = null!;
        public string CNPJ { get; private set; } = null!;
        public string? StateRegistration { get; private set; }
        public string? Address { get; private set; }
        public string? Phone { get; private set; }
        public string? Email { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        
        protected SupplierModel() { }
        
        public SupplierModel(string socialReason, string fantasyName, string cnpj)
        {
            if (string.IsNullOrWhiteSpace(socialReason))
                throw new ArgumentException("A razão social é obrigatória.");

            if (string.IsNullOrWhiteSpace(cnpj))
                throw new ArgumentException("O CNPJ é obrigatório.");

            SocialReason = socialReason;
            FantasyName = fantasyName;
            CNPJ = cnpj;
        }
        
        public void UpdateContact(string? phone, string? email)
        {
            Phone = phone;
            Email = email;
        }

        public void UpdateAddress(string? address)
        {
            Address = address;
        }

        public void UpdateStateRegistration(string? stateRegistration)
        {
            StateRegistration = stateRegistration;
        }
        
        public void SetUpdatedAt(DateTime updatedAt) => UpdatedAt = updatedAt;
        public void SetCreatedAt(DateTime createdAt) => CreatedAt = createdAt;
    }
}