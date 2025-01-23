using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagementSystem.Domain;

namespace OrderManagementSystem.Data.Customers;

/// <summary>
/// Конфигурация для сущности Customer.
/// </summary>
public sealed class CustomerEntityConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired();

        builder.Property(c => c.Code)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(c => c.Address)
            .HasMaxLength(200);

        builder.Property(c => c.Discount)
            .HasColumnType("decimal(5, 2)");
    }
}