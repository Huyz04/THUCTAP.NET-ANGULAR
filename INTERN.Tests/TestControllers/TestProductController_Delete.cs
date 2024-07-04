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

namespace INTERN.Tests.TestControllers
{
    public class TestProductController_Delete
    {
        private readonly ProductContext _context;
        private readonly IMapper _mapper;
        private readonly PProduct _pProduct;
        private readonly ProductController _productController;
        public TestProductController_Delete()
        {
            // Setup InMemory database
            var options = new DbContextOptionsBuilder<ProductContext>()
            .UseInMemoryDatabase(databaseName: "PrdDatabase")
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
        }
        [Fact]
        public async Task DeleteProduct_NoFindPrd()
        {

            // Act
            var result = await _productController.DeleteProduct(5);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Response>>(result);
            var response = Assert.IsType<Response>(actionResult.Value);
            Assert.False(response.Success);
            Assert.Equal("Have No Product Match With This ProductID!", response.Message);
        }
        [Fact]
        public async Task DeleteProduct_Successfully()
        {

            // Act
            var result = await _productController.DeleteProduct(1);

            // Assert
            //result.Should().BeNull();
            var actionResult = Assert.IsType<ActionResult<Response>>(result);
            var response = Assert.IsType<Response>(actionResult.Value);
            Assert.True(response.Success);
            Assert.Equal("Success!", response.Message);
        }

        [Fact]
        public async Task DeleteProduct_InvalidID_Failed()
        {

            // Act
            var result = await _productController.DeleteProduct(-1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Response>>(result);
            var response = Assert.IsType<Response>(actionResult.Value);
            Assert.False(response.Success);
            Assert.Equal("Invalid ID!", response.Message);
        }

        
    }
}
