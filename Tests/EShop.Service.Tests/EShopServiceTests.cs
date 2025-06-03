using Moq;
using Microsoft.AspNetCore.Mvc.Testing;
using EShop.Application.Service;
using EShopService.Controllers;
using EShopDomain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Service.Tests;

public class EShopServiceTests
{
    private readonly Mock<IProductService> _service;
    private readonly ProductController _controller;

    public EShopServiceTests()
    {
        _service = new Mock<IProductService>();
        _controller = new ProductController(_service.Object);
    }

    [Fact]
    public async Task Get_GetProductByName_ExpectedTwoProducts()  //po kompilacji dzia³a wszystko jak powinno. Niemniej, test nie chce dzia³aæ.
    {
        //Arrange
        var expected = new List<Product>{
                new Product { Name = "ProduktJeden" },
                new Product { Name = "ProduktDwa" }
        };
        var products = new List<Product>{
                new Product { Name = "GroduktTrzy" },
                new Product { Name = "RoduktCztery" }
        };
        products.AddRange(expected);
        _service.Setup(s => s.GetByNameAsync("Pr")).ReturnsAsync(products);
        //Act
        var response = await _controller.GetByName("Pr");
        //Assert
        var result = Assert.IsType<OkObjectResult>(response);
        Assert.Equal(expected, result.Value);
    }
}