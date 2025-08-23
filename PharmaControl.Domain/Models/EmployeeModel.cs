using PharmaControl.Common.Enuns;

namespace PharmaControl.Domain.Models
{
    public class EmployeeModel
    {
        public int IdEmployee { get; private set; }

        public string Name { get; private set; } = null!;
        public string CPF { get; private set; } = null!;
        public DateTime BirthDate { get; private set; }
        public RoleEnum Role { get; private set; } = RoleEnum.Atendente;
        public DateTime HiringDate { get; private set; }
        public string? Phone { get; private set; }
        public string? Email { get; private set; }
        public bool Status { get; private set; } = true;
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        protected EmployeeModel() { }

        public EmployeeModel(int idEmployee,string name, string cpf, DateTime birthDate, RoleEnum role, DateTime hiringDate)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("O nome do funcionário é obrigatório.");

            if (string.IsNullOrWhiteSpace(cpf))
                throw new ArgumentException("O CPF é obrigatório.");
            
            IdEmployee = idEmployee;
            Name = name;
            CPF = cpf;
            BirthDate = birthDate;
            Role = role;
            HiringDate = hiringDate;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("O nome não pode ser vazio.");

            Name = newName;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateHiringDate(DateTime newHiringDate)
        {
            if (newHiringDate > DateTime.UtcNow.Date)
                throw new ArgumentException("A data de contratação não pode ser no futuro.");

            HiringDate = newHiringDate.Kind == DateTimeKind.Utc
                ? newHiringDate
                : newHiringDate.ToUniversalTime();

            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateBirthDate(DateTime newBirthDate)
        {
            if (newBirthDate > DateTime.UtcNow.Date)
                throw new ArgumentException("A data de nascimento não pode ser no futuro.");

            BirthDate = newBirthDate.Kind == DateTimeKind.Utc
                ? newBirthDate
                : newBirthDate.ToUniversalTime();

            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateContact(string? phone, string? email)
        {
            Phone = phone;
            Email = email;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangeRole(RoleEnum newRole)
        {

            Role = newRole;
            UpdatedAt = DateTime.UtcNow;
        }
        
        public void SetIsActive(bool isActive)
        {
            Status = isActive;
            UpdatedAt = DateTime.UtcNow;
        }
        
        public void SetUpdatedAt(DateTime updatedAt) => UpdatedAt = updatedAt;
        public void SetCreatedAt(DateTime createdAt) => CreatedAt = createdAt;
    }
}