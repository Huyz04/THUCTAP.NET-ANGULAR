using AutoMapper;
using FakeItEasy;
using INTERN.Controllers;
using INTERN.DTO;
using INTERN.Model;
using INTERN.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Type = INTERN.Model.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;

namespace INTERN.Tests.TestControllers
{
    public class TestProductController_Put
    {
        private readonly ProductContext _context;
        private readonly IMapper _mapper;
        private readonly PProduct _pProduct;
        private readonly ProductController _productController;
        private readonly Mock<HttpContext> _mockHttpContext;

        public TestProductController_Put()
        {
            // Setup InMemory database
            var options = new DbContextOptionsBuilder<ProductContext>()
            .UseInMemoryDatabase(databaseName: "ProductDatabase")
            .Options;
            _context = new ProductContext(options);

            // Seed data

            var type1 = new Type
            {
                NameType = "Type1",
                Created_at = DateTime.Now,
                Created_by = "1",
                Updated_at = DateTime.Now,
                Updated_by = "1"
            };
            var product1 = new Product
            {
                Code = "P01",
                Name = "Product1",
                Type = type1,
                Description = "string",
                Price = 100,
                Created_at = DateTime.Now,
                Created_by = "1",
                Updated_at = DateTime.Now,
                Updated_by = "1"
            };
            _context.Products.Add(product1);
            _context.SaveChanges();
            // Setup AutoMapper
            _mapper = A.Fake<IMapper>();

            // Initialize PProduct with the real context and mapper
            _pProduct = new PProduct(_context, _mapper);
            _productController = new ProductController(_pProduct);

            // Setup mock HttpContext
            _mockHttpContext = new Mock<HttpContext>();
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1")
            }));
            _mockHttpContext.Setup(x => x.User).Returns(claimsPrincipal);
        }

        [Fact]
        public async Task PutProduct_IdNotMatch_ReturnsIdNotMatchResponse()
        {
            // Arrange
            var productDto = new ProductDTO
            {
                Id = 99,
                Code = "P03",
                Name = "Product3",
                TypeName = "Type1",
                Description = "string",
                Price = 150
            };

            // Act
            var result = await _pProduct.PutProduct(1, productDto, _mockHttpContext.Object);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Response>>(result);
            var response = Assert.IsType<Response>(actionResult.Value);
            Assert.False(response.Success);
            Assert.Equal("The ID Not Match!", response.Message);
        }

        [Fact]
        public async Task PutProduct_ProductNotFound_ReturnsProductNotFoundResponse()
        {
            // Arrange
            var productDto = new ProductDTO
            {
                Id = 99,
                Code = "P03",
                Name = "Product3",
                TypeName = "Type1",
                Description = "string",
                Price = 150
            };

            // Act
            var result = await _pProduct.PutProduct(99, productDto, _mockHttpContext.Object);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Response>>(result);
            var response = Assert.IsType<Response>(actionResult.Value);
            Assert.False(response.Success);
            Assert.Equal("Have no Product with this Id!", response.Message);
        }

        [Fact]
        public async Task PutProduct_NoMatchingType_ReturnsNoMatchingTypeResponse()
        {
            // Arrange
            var productDto = new ProductDTO
            {
                Id = 1,
                Code = "P03",
                Name = "Product3",
                TypeName = null,
                Description = "string",
                Price = 150
            };

            // Act
            var result = await _pProduct.PutProduct(1, productDto, _mockHttpContext.Object);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Response>>(result);
            var response = Assert.IsType<Response>(actionResult.Value);
            Assert.False(response.Success);
            Assert.Equal("TypeName cannot null!", response.Message);
        }

        [Fact]
        public async Task PutProduct_ValidProduct_ReturnsSuccessResponse()
        {
            var type2 = new Type
            {
                NameType = "Type2",
                Created_at = DateTime.Now,
                Created_by = "1",
                Updated_at = DateTime.Now,
                Updated_by = "1"
            };
            var product1 = new Product
            {
                Code = "P01",
                Name = "Product2",
                Type = type2,
                Description = "string",
                Price = 100,
                Created_at = DateTime.Now,
                Created_by = "1",
                Updated_at = DateTime.Now,
                Updated_by = "1"
            };
            _context.Products.Add(product1);
            _context.SaveChanges();
            // Arrange
            var productDto = new ProductDTO
            {
                Id = 2,
                Code = "P03",
                Name = "Product3",
                TypeName = "Type1",
                Description = "string",
                Price = 150
            };

            // Act
            var result = await _pProduct.PutProduct(2, productDto, _mockHttpContext.Object);

            // Assert
            //result.Should().BeNull();
            var actionResult = Assert.IsType<ActionResult<Response>>(result);
            var response = Assert.IsType<Response>(actionResult.Value);
            Assert.True(response.Success);
            Assert.Equal("Success!", response.Message);
        }
    }
}
