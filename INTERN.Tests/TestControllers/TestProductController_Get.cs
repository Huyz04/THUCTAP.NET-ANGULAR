using INTERN.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using FakeItEasy;
using INTERN.Providers;
using INTERN.Controllers;
using INTERN.DTO;
using FluentAssertions;
using INTERN.Helper;

namespace INTERN.Tests.TestControllers
{
    public class TestProductController_Get
    {
        private readonly ProductContext _productContext1;
        private readonly IMapper _mapper1;
        private readonly PProduct _pProduct1;
        public TestProductController_Get()
        {
            var options = new DbContextOptionsBuilder<ProductContext>()
                .UseInMemoryDatabase(databaseName: "ProductDatabase")
                .Options;

            _productContext1 = new ProductContext(options);
            _mapper1 = A.Fake<IMapper>();
            _pProduct1 = new PProduct(_productContext1, _mapper1);
        }

        [Fact]
        public async Task GetProduct_ReturnsProducts_WhenNoFilterApplied()
        {
            //Arrange
            var product = A.Fake<ICollection<ProductDTO>>();
            var productList = A.Fake<IEnumerable<ProductDTO>>();
            A.CallTo(() => _mapper1.Map<IEnumerable<ProductDTO>>(product)).Returns(productList);
            var controller = new ProductController(_productContext1, _pProduct1, _mapper1);

            //Act
            var result = await controller.GetProducts(null, null, 1, 10);

            //Assert
            //result.Should().BeNull();                                       // To debug when test failed.
            Assert.IsType<ActionResult<Response>>(result);
            Assert.True(result.Value.Success);
            Assert.Equal("Success!", result.Value.Message);

        }

        [Fact]
        public async Task GetProductWithID_ReturnsProducts_WhenIdNotTrue()
        {
            //Arrange
            var product = A.Fake<ICollection<ProductDTO>>();
            var productList = A.Fake<IEnumerable<ProductDTO>>();
            A.CallTo(() => _mapper1.Map<IEnumerable<ProductDTO>>(product)).Returns(productList);
            var controller = new ProductController(_productContext1, _pProduct1, _mapper1);

            //Act
            var result = await controller.GetProduct(-1);

            //Assert
            //result.Should().BeNull();                                       // To debug when test failed.
            Assert.IsType<ActionResult<Response>>(result);
            Assert.False(result.Value.Success);
            Assert.Equal("Invalid ID!", result.Value.Message);

        }

        [Fact]
        public async Task GetProductWithID_ReturnsProducts_WhenIdTrue()
        {
            //Arrange
            var product = A.Fake<ICollection<ProductDTO>>();
            var productList = A.Fake<IEnumerable<ProductDTO>>();
            A.CallTo(() => _mapper1.Map<IEnumerable<ProductDTO>>(product)).Returns(productList);
            var controller = new ProductController(_productContext1, _pProduct1, _mapper1);

            //Act
            var result = await controller.GetProduct(1);

            //Assert
            //result.Should().BeNull();                                       // To debug when test failed.
            Assert.IsType<ActionResult<Response>>(result);
            Assert.True(result.Value.Success);
            Assert.Equal(1, result.Value.data.Total);
            Assert.Equal("Success!", result.Value.Message);

        }

        [Fact]
        public async Task GetProductFindPage_FiltersByName()
        {
            //Arrange
            var product = A.Fake<ICollection<ProductDTO>>();
            var productList = A.Fake<IEnumerable<ProductDTO>>();
            A.CallTo(() => _mapper1.Map<IEnumerable<ProductDTO>>(product)).Returns(productList);
            var controller = new ProductController(_productContext1, _pProduct1, _mapper1);

            //Act
            var result = await controller.GetProducts("Product", "Name", 1, 10);

            //Assert
            //result.Should().BeNull();                                       // To debug when test failed.
            Assert.IsType<ActionResult<Response>>(result);
            Assert.True(result.Value.Success);
            Assert.Equal("Success!", result.Value.Message);
        }


    }
}
