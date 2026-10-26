using System.Reflection;
using Acacia.Api.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Acacia.Tests.Security;

public class SecurityAuditTests
{
    [Theory]
    [InlineData(typeof(FinalProductsController), nameof(FinalProductsController.Create))]
    [InlineData(typeof(FinalProductsController), nameof(FinalProductsController.Delete))]
    [InlineData(typeof(BottleDesignsController), nameof(BottleDesignsController.CreateBottleDesign))]
    [InlineData(typeof(BottleDesignsController), nameof(BottleDesignsController.UpdateBottleDesign))]
    [InlineData(typeof(BottleDesignsController), nameof(BottleDesignsController.DeleteBottleDesign))]
    [InlineData(typeof(CompaniesController), nameof(CompaniesController.Create))]
    [InlineData(typeof(CompaniesController), nameof(CompaniesController.Update))]
    [InlineData(typeof(CompaniesController), nameof(CompaniesController.Delete))]
    [InlineData(typeof(IngredientsController), nameof(IngredientsController.Create))]
    [InlineData(typeof(IngredientsController), nameof(IngredientsController.Update))]
    [InlineData(typeof(IngredientsController), nameof(IngredientsController.Delete))]
    public void MutationEndpoints_MustRequireAdministratorRole(Type controllerType, string methodName)
    {
        // Arrange
        var method = controllerType.GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);
        method.Should().NotBeNull($"Method {methodName} must exist on {controllerType.Name}");

        // Act
        var authAttr = method!.GetCustomAttribute<AuthorizeAttribute>();

        // Assert - OWASP A01: Broken Access Control Defense
        authAttr.Should().NotBeNull($"Method {methodName} on {controllerType.Name} must be decorated with [Authorize]");
        authAttr!.Roles.Should().Contain("Administrator", "Mutation endpoints must be restricted to Administrator role");
    }

    [Theory]
    [InlineData(typeof(FinalProductsController), nameof(FinalProductsController.GetAll))]
    [InlineData(typeof(FinalProductsController), nameof(FinalProductsController.GetById))]
    [InlineData(typeof(BottleDesignsController), nameof(BottleDesignsController.GetAllBottleDesigns))]
    [InlineData(typeof(CompaniesController), nameof(CompaniesController.GetAll))]
    [InlineData(typeof(IngredientsController), nameof(IngredientsController.GetAll))]
    public void ReadEndpoints_MustBePubliclyReadable(Type controllerType, string methodName)
    {
        // Arrange
        var method = controllerType.GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);
        method.Should().NotBeNull();

        // Act
        var authAttr = method!.GetCustomAttribute<AuthorizeAttribute>();

        // Assert - Catalog reads are accessible to guests and consumers
        authAttr.Should().BeNull("Read/Catalog endpoints should be accessible for storefront browsing");
    }
}
