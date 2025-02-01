using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagementSystem.Domain;

namespace OrderManagementSystem.Data.OrderItems;

/// <summary>
/// Конфигурация для сущности OrderItem.
/// </summary>
public sealed class OrderItemEntityConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(oi => oi.Id);

        builder.Property(oi => oi.ItemsCount)
            .IsRequired();

        builder.Property(oi => oi.ItemPrice)
            .IsRequired()
            .HasColumnType("decimal(10, 2)");
        
        builder.Property(c => c.OrderId)
            .ValueGeneratedNever();

        builder.HasOne<Order>()
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(oi => oi.ItemId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}