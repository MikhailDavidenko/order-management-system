using FluentAssertions;
using OrderManagementSystem.Domain;
using Xunit;

namespace OrderManagementSystem.DomainTests;

public sealed class ProductTests
{
    [Fact]
    public void CreateProduct_ValidData_CreatesProduct()
    {
        // Arrange
        string name = "Test Product";
        decimal price = 10.99m;
        string category = "Test Category";

        // Act
        var product = Product.Create(name, price, category);

        // Assert
        product.Should().NotBeNull();
        product.Name.Should().Be(name);
        product.Price.Should().Be(price);
        product.Category.Should().Be(category);
    }

    [Fact]
    public void CreateProduct_InvalidPrice_ThrowsException()
    {
        // Arrange
        string name = "Test Product";
        decimal price = 0; // Неправильная цена
        string category = "Test Category";

        // Act
        Action act = () => Product.Create(name, price, category);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void UpdateProduct_ValidData_UpdatesProduct()
    {
        // Arrange
        var product = Product.Create("Initial Product", 10.99m, "Test Category");
        string newName = "Updated Product";
        string newCode = "PROD-001";
        decimal newPrice = 15.99m;
        string newCategory = "Updated Category";

        // Act
        product.Update(newName, newCode, newPrice, newCategory);

        // Assert
        product.Name.Should().Be(newName);
        product.Code.Should().Be(newCode);
        product.Price.Should().Be(newPrice);
        product.Category.Should().Be(newCategory);
    }

    [Fact]
    public void UpdateProduct_InvalidPrice_ThrowsException()
    {
        // Arrange
        var product = Product.Create("Initial Product", 10.99m, "Test Category");
        decimal newPrice = 0; // Неправильная цена

        // Act
        Action act = () => product.Update("Updated Product", "PROD-001", newPrice, "Updated Category");

        // Assert
        act.Should().Throw<ArgumentException>();
    }
}