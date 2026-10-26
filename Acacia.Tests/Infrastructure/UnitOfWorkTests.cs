using Acacia.Infrastructure.Context;
using Acacia.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Acacia.Tests.Infrastructure;

public class UnitOfWorkTests
{
    private readonly AcaciaDbContext _context;
    private readonly UnitOfWork _unitOfWork;

    public UnitOfWorkTests()
    {
        var options = new DbContextOptionsBuilder<AcaciaDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AcaciaDbContext(options);
        _unitOfWork = new UnitOfWork(_context);
    }

    [Fact]
    public void Repositories_WhenAccessedMultipleTimes_ShouldReturnSameCachedInstance()
    {
        // Act
        var repo1 = _unitOfWork.oilRepository;
        var repo2 = _unitOfWork.oilRepository;

        var bottle1 = _unitOfWork.bottleDesignRepository;
        var bottle2 = _unitOfWork.bottleDesignRepository;

        var product1 = _unitOfWork.finalProductRepository;
        var product2 = _unitOfWork.finalProductRepository;

        // Assert - Backing fields prevent re-allocating new repository instances
        repo1.Should().BeSameAs(repo2);
        bottle1.Should().BeSameAs(bottle2);
        product1.Should().BeSameAs(product2);
    }

    [Fact]
    public async Task SaveChangesAsync_ShouldExecuteSuccessfullyOnContext()
    {
        // Act
        var result = await _unitOfWork.SaveChangesAsync();

        // Assert
        result.Should().Be(0); // empty in-memory DB has 0 changes
    }
}
