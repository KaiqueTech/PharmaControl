namespace PharmaControl.Domain.Models
{
    public class ProductModel
    {
        public int IdProduct { get; private set; }

        public string Name { get; private set; } = null!;
        public string Category { get; private set; } = null!;
        public string Manufacturer { get; private set; } = null!;
        public string Batch { get; private set; } = null!;  // Lote
        public int Quantity { get; private set; }
        public DateTime ManufactureDate { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        
        public int SupplierId { get; private set; }
        public SupplierModel Supplier { get; private set; } = null!;

        protected ProductModel() { }

        public ProductModel(string name, string category, string manufacturer, string batch,
                            int quantity, DateTime manufactureDate, DateTime expirationDate, int supplierId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("O nome do produto é obrigatório.");

            if (string.IsNullOrWhiteSpace(category))
                throw new ArgumentException("A categoria é obrigatória.");

            if (string.IsNullOrWhiteSpace(manufacturer))
                throw new ArgumentException("O fabricante é obrigatório.");

            if (quantity < 0)
                throw new ArgumentException("A quantidade não pode ser negativa.");

            if (manufactureDate > DateTime.UtcNow.Date)
                throw new ArgumentException("A data de fabricação não pode ser futura.");

            if (expirationDate <= manufactureDate)
                throw new ArgumentException("A data de validade deve ser maior que a de fabricação.");

            Name = name;
            Category = category;
            Manufacturer = manufacturer;
            Batch = batch;
            Quantity = quantity;
            ManufactureDate = manufactureDate;
            ExpirationDate = expirationDate;
            SupplierId = supplierId;
        }
        
        public void AddStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("A quantidade a adicionar deve ser maior que zero.");

            Quantity += quantity;
        }

        public void RemoveStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("A quantidade a remover deve ser maior que zero.");

            if (quantity > Quantity)
                throw new InvalidOperationException("Não há estoque suficiente.");

            Quantity -= quantity;
        }

        public bool IsExpired() => DateTime.UtcNow.Date > ExpirationDate.Date;

        public bool ExpiresInNextDays(int days)
        {
            var today = DateTime.UtcNow.Date;
            return ExpirationDate.Date <= today.AddDays(days) && ExpirationDate.Date >= today;
        }
        
        public void SetUpdatedAt(DateTime updatedAt) => UpdatedAt = updatedAt;
        public void SetCreatedAt(DateTime createdAt) => CreatedAt = createdAt;
    }
}
