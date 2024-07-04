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
    public class TestProductController_Post
    {
        private readonly ProductContext _context;
        private readonly IMapper _mapper;
        private readonly PProduct _pProduct;
        private readonly ProductController _productController;
        private readonly Mock<HttpContext> _mockHttpContext;

        public TestProductController_Post()
        {
            // Setup InMemory database
            var options = new DbContextOptionsBuilder<ProductContext>()
            .UseInMemoryDatabase(databaseName: "ProductDatabase")
            .Options;
            _context = new ProductContext(options);

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
        public async Task PostProduct_InvalidId_ReturnsInvalidIdResponse()
        {
            // Arrange
            var productDto = new ProductDTO { 
                Id = 3,
                Code = "P02",
                Name = "Product1", 
                TypeName = "Type1",
                Description = "string",
                Price = 100
            };

            // Act
            var result = await _pProduct.PostProduct(productDto, _mockHttpContext.Object);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Response>>(result);
            var response = Assert.IsType<Response>(actionResult.Value);
            Assert.False(response.Success);
            Assert.Equal("Please do not fill in the ID field!", response.Message);
        }
        [Fact]
        public async Task PostProduct_ValidProduct_ReturnsSuccessResponse()
        {
            // Arrange
            var productDto = new ProductDTO { 
                Id = 0,
                Code = "P03",
                Name = "Product3",
                TypeName = "Type1",
                Description = "string",
                Price = 150
            };

            // Act
            var result = await _pProduct.PostProduct(productDto, _mockHttpContext.Object);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Response>>(result);
            var response = Assert.IsType<Response>(actionResult.Value);
            Assert.True(response.Success);
        }

        [Fact]
        public async Task PostProduct_NoMatchingType_ReturnsErrorResponse()
        {
            // Arrange
            var productDto = new ProductDTO { 
                Id = 0,
                Code = "P04",
                Name = "Product4",
                Description = "string",
                Price = 150,
                TypeName = null
            };

            // Act
            var result = await _pProduct.PostProduct(productDto, _mockHttpContext.Object);

            // Assert
            //result.Should().BeNull();
            var actionResult = Assert.IsType<ActionResult<Response>>(result);
            var response = Assert.IsType<Response>(actionResult.Value);
            Assert.False(response.Success);
            Assert.Equal("TypeName cannot null!", response.Message);
        }
    }
}
