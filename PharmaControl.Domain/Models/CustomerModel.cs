namespace PharmaControl.Domain.Models
{
    public class CustomerModel
    {
        public int IdCustomer { get; private set; }

        public string Name { get; private set; } = null!;
        public string CPF { get; private set; } = null!;
        public DateTime BirthDate { get; private set; }
        public string? Address { get; private set; }
        public string? Phone { get; private set; }
        public string? Email { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        protected CustomerModel() { }

        public CustomerModel(string name, string cpf, DateTime birthDate)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("O nome do cliente é obrigatório.");

            if (string.IsNullOrWhiteSpace(cpf))
                throw new ArgumentException("O CPF é obrigatório.");

            if (birthDate > DateTime.UtcNow.Date)
                throw new ArgumentException("A data de nascimento não pode ser no futuro.");

            Name = name;
            CPF = cpf;
            BirthDate = birthDate;
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
        
        public void SetUpdatedAt(DateTime updatedAt) => UpdatedAt = updatedAt;
        public void SetCreatedAt(DateTime createdAt) => CreatedAt = createdAt;
    }
}