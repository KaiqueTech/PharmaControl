using Microsoft.EntityFrameworkCore;
using PharmaControl.Domain.Models;

namespace PharmaControl.Infrastructure.DataContext;

public class PharmaDbContext : DbContext
{
    public PharmaDbContext(DbContextOptions<PharmaDbContext> options) : base(options)
    {
    }
    
    public DbSet<CustomerModel> Customers { get; set; }
    public DbSet<SupplierModel> Suppliers { get; set; }
    public DbSet<EmployeeModel> Employees { get; set; }
    public DbSet<ProductModel> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerModel>(entity =>
        {
            entity.ToTable("tb_Customers");
            
            entity.HasKey(c => c.IdCustomer);
            entity.Property(c => c.IdCustomer).ValueGeneratedOnAdd();

            entity.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(c => c.CPF)
                .IsRequired()
                .HasMaxLength(11);

            entity.Property(c => c.BirthDate)
                .IsRequired();

            entity.Property(c => c.Address).HasMaxLength(250);
            entity.Property(c => c.Phone).HasMaxLength(20);
            entity.Property(c => c.Email).HasMaxLength(120);
        });

        modelBuilder.Entity<SupplierModel>(entity =>
        {
            entity.ToTable("tb_Suppliers");

            entity.HasKey(s => s.IdSupplier);
            entity.Property(p => p.IdSupplier).ValueGeneratedOnAdd();

            entity.Property(s => s.SocialReason)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(s => s.FantasyName)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(s => s.CNPJ)
                .IsRequired()
                .HasMaxLength(14);

            entity.Property(s => s.StateRegistration).HasMaxLength(50);
            entity.Property(s => s.Address).HasMaxLength(250);
            entity.Property(s => s.Phone).HasMaxLength(20);
            entity.Property(s => s.Email).HasMaxLength(120);
        });

        modelBuilder.Entity<EmployeeModel>(entity =>
        {
            entity.ToTable("tb_Employees");

            entity.HasKey(e => e.IdEmployee);
            entity.Property(p => p.IdEmployee).ValueGeneratedOnAdd();

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.CPF)
                .IsRequired()
                .HasMaxLength(11);

            entity.Property(e => e.Role)
                .IsRequired()
                .HasConversion<int>();
            
            entity.Property(e => e.Status)
                .IsRequired()
                .HasConversion<int>();
            
            entity.Property(e => e.BirthDate)
                .HasColumnType("timestamp with time zone");
            entity.Property(e => e.HiringDate)
                .HasColumnType("timestamp with time zone");

            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Email).HasMaxLength(120);
            
            
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp with time zone");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp with time zone");
        });

        modelBuilder.Entity<ProductModel>(entity =>
        {
            entity.ToTable("tb_Products");

            entity.HasKey(p => p.IdProduct);
            entity.Property(p => p.IdProduct).ValueGeneratedOnAdd();

            entity.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(p => p.Category)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(p => p.Manufacturer)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(p => p.Batch)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(p => p.Quantity).IsRequired();

            entity.Property(p => p.ManufactureDate).IsRequired();
            entity.Property(p => p.ExpirationDate).IsRequired();


            entity.HasOne(p => p.Supplier)
                .WithMany()
                .HasForeignKey(p => p.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    public override int SaveChanges()
    {
        UpdateDates();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateDates();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateDates()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entityEntry in entries)
        {
            switch (entityEntry.Entity)
            {
                case CustomerModel customer:
                    customer.SetUpdatedAt(DateTime.UtcNow);
                    if (entityEntry.State == EntityState.Added)
                        customer.SetCreatedAt(DateTime.UtcNow);
                    break;

                case EmployeeModel employee:
                    employee.SetUpdatedAt(DateTime.UtcNow);
                    if (entityEntry.State == EntityState.Added)
                        employee.SetCreatedAt(DateTime.UtcNow);
                    break;
                
                case SupplierModel supplier:
                    supplier.SetUpdatedAt(DateTime.UtcNow);
                    if (entityEntry.State == EntityState.Added)
                        supplier.SetCreatedAt(DateTime.UtcNow);
                    break;
                
                case ProductModel product:
                    product.SetUpdatedAt(DateTime.UtcNow);
                    if (entityEntry.State == EntityState.Added)
                        product.SetCreatedAt(DateTime.UtcNow);
                    break;
            }
        }
    }
}