using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagementSystem.Domain;

namespace OrderManagementSystem.Data.Orders;

/// <summary>
/// Конфигурация для сущности Order.
/// </summary>
public sealed class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.OrderDate)
            .IsRequired();

        builder.Property(o => o.ShipmentDate)
            .IsRequired(false);

        builder.Property(o => o.OrderNumber)
            .IsRequired();

        builder.Property(o => o.Status)
            .IsRequired();
        
        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}