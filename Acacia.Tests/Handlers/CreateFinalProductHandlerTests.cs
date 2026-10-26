using System.Net;
using Acacia.Core.Features.FinalProducts;
using Acacia.Core.Features.FinalProducts.Commands.CreateFinalProduct;
using Acacia.Core.Interfaces.IReposetories;
using Acacia.Core.Interfaces.IReposetories.IFinalProducts;
using Acacia.Core.Resources;
using Acacia.Data.Entities;
using Acacia.Data.Entities.FinalProducts;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Localization;
using Moq;
using Xunit;

namespace Acacia.Tests.Handlers;

public class CreateFinalProductHandlerTests
{
    private readonly Mock<IUnitOfWork> _mockUow = new();
    private readonly Mock<IOilRepository> _mockOilRepo = new();
    private readonly Mock<IProductTypeRepository> _mockTypeRepo = new();
    private readonly Mock<IProductSizeRepository> _mockSizeRepo = new();
    private readonly Mock<IBottleDesignRepository> _mockBottleRepo = new();
    private readonly Mock<IFinalProductRepository> _mockFinalProductRepo = new();
    private readonly Mock<IMapper> _mockMapper = new();
    private readonly Mock<IStringLocalizer<SharedResources>> _mockLocalizer = new();

    private readonly CreateFinalProductHandler _handler;

    public CreateFinalProductHandlerTests()
    {
        _mockUow.Setup(u => u.oilRepository).Returns(_mockOilRepo.Object);
        _mockUow.Setup(u => u.productTypeRepository).Returns(_mockTypeRepo.Object);
        _mockUow.Setup(u => u.productSizeRepository).Returns(_mockSizeRepo.Object);
        _mockUow.Setup(u => u.bottleDesignRepository).Returns(_mockBottleRepo.Object);
        _mockUow.Setup(u => u.finalProductRepository).Returns(_mockFinalProductRepo.Object);

        _mockLocalizer.Setup(l => l[It.IsAny<string>()]).Returns(new LocalizedString("Key", "Value"));

        _handler = new CreateFinalProductHandler(
            _mockUow.Object,
            _mockMapper.Object,
            _mockLocalizer.Object
        );
    }

    [Fact]
    public async Task Handle_WhenAllReferencesExist_ShouldCreatePerfumeFinalProductAndReturnCreated()
    {
        // Arrange
        var command = new CreateFinalProductCommand
        {
            OilId = 1,
            ProductTypeId = 1, // Perfume
            ProductSizeId = 1,
            BottleDesignId = 1,
            BasePrice = 30.0m,
            TotalPrice = 95.0m,
            AlcoholPercentage = 22.5m
        };

        _mockOilRepo.Setup(r => r.ExistsAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(true);
        _mockTypeRepo.Setup(r => r.ExistsAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(true);
        _mockSizeRepo.Setup(r => r.ExistsAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(true);
        _mockBottleRepo.Setup(r => r.ExistsAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(true);

        FinalProduct? capturedEntity = null;
        _mockFinalProductRepo
            .Setup(r => r.AddAsync(It.IsAny<FinalProduct>(), It.IsAny<CancellationToken>()))
            .Callback<FinalProduct, CancellationToken>((entity, _) => capturedEntity = entity)
            .ReturnsAsync((FinalProduct entity, CancellationToken _) => entity);

        _mockUow.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var expectedDto = new FinalProductResponse
        {
            Id = 10,
            OilId = 1,
            OilName = "Royal Amber",
            BasePrice = 30.0m,
            TotalPrice = 95.0m,
            AlcoholPercentage = 22.5m
        };

        _mockMapper.Setup(m => m.Map<FinalProductResponse>(It.IsAny<object>())).Returns(expectedDto);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Response_Code.Should().Be(HttpStatusCode.Created);
        result.Succeeded.Should().BeTrue();
        result.Data.Should().NotBeNull();
        result.Data!.TotalPrice.Should().Be(95.0m);

        capturedEntity.Should().NotBeNull();
        capturedEntity.Should().BeOfType<PerfumeFinalProduct>();
        ((PerfumeFinalProduct)capturedEntity!).AlcoholPercentage.Should().Be(22.5m);
        capturedEntity.BasePrice.Should().Be(30.0m);
        capturedEntity.TotalPrice.Should().Be(95.0m);

        _mockUow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenOilDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var command = new CreateFinalProductCommand { OilId = 999 };
        _mockOilRepo.Setup(r => r.ExistsAsync(999, It.IsAny<CancellationToken>())).ReturnsAsync(false);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Response_Code.Should().Be(HttpStatusCode.NotFound);
        result.Succeeded.Should().BeFalse();
        result.Errors.Should().ContainKey("OilId");
        _mockFinalProductRepo.Verify(r => r.AddAsync(It.IsAny<FinalProduct>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_WhenBottleDesignDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var command = new CreateFinalProductCommand
        {
            OilId = 1,
            ProductTypeId = 1,
            ProductSizeId = 1,
            BottleDesignId = 888
        };

        _mockOilRepo.Setup(r => r.ExistsAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(true);
        _mockTypeRepo.Setup(r => r.ExistsAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(true);
        _mockSizeRepo.Setup(r => r.ExistsAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(true);
        _mockBottleRepo.Setup(r => r.ExistsAsync(888, It.IsAny<CancellationToken>())).ReturnsAsync(false);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Response_Code.Should().Be(HttpStatusCode.NotFound);
        result.Succeeded.Should().BeFalse();
        result.Errors.Should().ContainKey("BottleDesignId");
    }
}
