using Acacia.Core.Features.FinalProducts.Commands.CreateFinalProduct;
using FluentAssertions;
using Xunit;

namespace Acacia.Tests.Validators;

public class CreateFinalProductValidatorTests
{
    private readonly CreateFinalProductValidator _validator = new();

    [Fact]
    public void Validate_ValidCommand_ShouldNotHaveValidationErrors()
    {
        // Arrange
        var command = new CreateFinalProductCommand
        {
            OilId = 1,
            ProductTypeId = 1,
            ProductSizeId = 1,
            BottleDesignId = 1,
            BasePrice = 25.00m,
            TotalPrice = 85.00m,
            AlcoholPercentage = 20.0m
        };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Theory]
    [InlineData(0, 1, 1, 1, 10, 30, "OilId")]
    [InlineData(1, 0, 1, 1, 10, 30, "ProductTypeId")]
    [InlineData(1, 1, 0, 1, 10, 30, "ProductSizeId")]
    [InlineData(1, 1, 1, 0, 10, 30, "BottleDesignId")]
    [InlineData(1, 1, 1, 1, -1, 30, "BasePrice")]
    [InlineData(1, 1, 1, 1, 10, -5, "TotalPrice")]
    public void Validate_InvalidFields_ShouldFailValidation(
        int oilId, int typeId, int sizeId, int bottleId, decimal basePrice, decimal totalPrice, string expectedErrorField)
    {
        // Arrange
        var command = new CreateFinalProductCommand
        {
            OilId = oilId,
            ProductTypeId = typeId,
            ProductSizeId = sizeId,
            BottleDesignId = bottleId,
            BasePrice = basePrice,
            TotalPrice = totalPrice
        };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == expectedErrorField);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(101)]
    public void Validate_AlcoholPercentageOutOfRange_ShouldFailValidation(decimal alcohol)
    {
        // Arrange
        var command = new CreateFinalProductCommand
        {
            OilId = 1,
            ProductTypeId = 1,
            ProductSizeId = 1,
            BottleDesignId = 1,
            BasePrice = 20,
            TotalPrice = 60,
            AlcoholPercentage = alcohol
        };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "AlcoholPercentage");
    }
}
