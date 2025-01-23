using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagementSystem.Domain;

namespace OrderManagementSystem.Data.Products;

/// <summary>
/// Конфигурация для сущности Product.
/// </summary>
public sealed class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Code)
            .IsRequired()
            .HasMaxLength(15);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Price)
            .IsRequired()
            .HasColumnType("decimal(10, 2)");

        builder.Property(p => p.Category)
            .HasMaxLength(50);
    }
}