using FluentAssertions;
using OrderManagementSystem.Domain;
using Xunit;

namespace OrderManagementSystem.DomainTests;

public sealed class CustomerTests
{
    [Fact]
    public void CreateCustomer_ValidData_CreatesCustomer()
    {
        // Arrange
        string name = "Test Customer";
        string address = "123 Test St";
        decimal? discount = 10;

        // Act
        var customer = Customer.Create(name, address, discount);

        // Assert
        customer.Should().NotBeNull();
        customer.Name.Should().Be(name);
        customer.Address.Should().Be(address);
        customer.Discount.Should().Be(discount);
    }

    [Fact]
    public void CreateCustomer_InvalidDiscount_ThrowsException()
    {
        // Arrange
        string name = "Test Customer";
        string address = "123 Test St";
        decimal? discount = 150;

        // Act
        Action act = () => Customer.Create(name, address, discount);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void UpdateCustomer_ValidData_UpdatesCustomer()
    {
        // Arrange
        var customer = Customer.Create("Initial Name", "Initial Address", null);
        string newName = "Updated Name";
        string newCode = "CUST-001";
        string newAddress = "Updated Address";
        decimal? newDiscount = 5;

        // Act
        customer.Update(newName, newCode, newAddress, newDiscount);

        // Assert
        customer.Name.Should().Be(newName);
        customer.Code.Should().Be(newCode);
        customer.Address.Should().Be(newAddress);
        customer.Discount.Should().Be(newDiscount);
    }

    [Fact]
    public void UpdateCustomer_InvalidDiscount_ThrowsException()
    {
        // Arrange
        var customer = Customer.Create("Initial Name", "Initial Address", null);
        string newName = "Updated Name";
        decimal? newDiscount = 150;

        // Act
        Action act = () => customer.Update(newName, "CUST-001", "New Address", newDiscount);

        // Assert
        act.Should().Throw<ArgumentException>();
    }
}