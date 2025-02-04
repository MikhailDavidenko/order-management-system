using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using OrderManagementSystem.Domain;
using Xunit;

namespace OrderManagementSystem.DomainTests;

public sealed class OrderTests
{
    [Fact]
    public void CreateOrder_ValidData_CreatesOrder()
    {
        // Arrange
        Guid customerId = Guid.NewGuid();
        int orderNumber = 1;

        // Act
        var order = Order.Create(customerId, orderNumber);

        // Assert
        order.Should().NotBeNull();
        order.CustomerId.Should().Be(customerId);
        order.OrderNumber.Should().Be(orderNumber);
        order.Status.Should().Be(OrderStatus.New);
    }

    [Fact]
    public void UpdateOrderStatus_ValidStatus_UpdatesStatus()
    {
        // Arrange
        var order = Order.Create(Guid.NewGuid(), 1);
        order.UpdateStatus(OrderStatus.InProgress);

        // Act
        order.UpdateStatus(OrderStatus.Completed);

        // Assert
        order.Status.Should().Be(OrderStatus.Completed);
    }

    [Fact]
    public void UpdateOrderStatus_InvalidTransition_ThrowsException()
    {
        // Arrange
        var order = Order.Create(Guid.NewGuid(), 1);
        order.UpdateStatus(OrderStatus.InProgress);

        // Act
        Action act = () => order.UpdateStatus(OrderStatus.New);

        // Assert
        act.Should().Throw<ValidationException>();
    }

    [Fact]
    public void UpdateShipmentDate_ValidDate_UpdatesDate()
    {
        // Arrange
        var order = Order.Create(Guid.NewGuid(), 1);
        DateTime shipmentDate = DateTime.UtcNow.AddDays(1);

        // Act
        order.Update(shipmentDate);

        // Assert
        order.ShipmentDate.Should().Be(shipmentDate.ToUniversalTime());
    }

    [Fact]
    public void UpdateShipmentDate_InvalidDate_ThrowsException()
    {
        // Arrange
        var order = Order.Create(Guid.NewGuid(), 1);
        DateTime invalidShipmentDate = order.OrderDate.AddDays(-1); // Дата доставки раньше даты заказа

        // Act
        Action act = () => order.Update(invalidShipmentDate);

        // Assert
        act.Should().Throw<ValidationException>();
    }
}

