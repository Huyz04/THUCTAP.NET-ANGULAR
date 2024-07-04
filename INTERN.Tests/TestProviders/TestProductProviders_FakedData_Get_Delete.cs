using INTERN.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using FakeItEasy;
using INTERN.Providers;
using INTERN.DTO;
using FluentAssertions;

namespace INTERN.Tests.TestProviders
{
    public class TestProductProviders_FakedData_Get_Delete
    {
        private readonly ProductContext _productContext;
        private readonly IMapper _mapper;
        //private readonly PProduct _pProduct;
        public TestProductProviders_FakedData_Get_Delete()
        {
            var options = new DbContextOptionsBuilder<ProductContext>()
                .UseInMemoryDatabase(databaseName: "ProductDatabase")
                .Options;

            _productContext = A.Fake<ProductContext>();
            _mapper = A.Fake<IMapper>();
            
        }

        [Fact]
        public async Task GetProductFindPage_ReturnsProducts_WhenNoFilterApplied()
        {
            //Arrange
            var product = A.Fake<ICollection<ProductDTO>>();
            var productList = A.Fake<IEnumerable<ProductDTO>>();
            A.CallTo(() => _mapper.Map<IEnumerable<ProductDTO>>(product)).Returns(productList);
            var controller = new PProduct(_productContext, _mapper);

            //Act
            var result = await controller.GetProductFindPage(null, null, 1, 10);

            //Assert
            //result.Should().BeNull();                                       // To debug when test failed.
            Assert.IsType<ActionResult<Response>>(result);
            Assert.True(result.Value.Success);

        }
        [Fact]
        public async Task GetProductWithID_ReturnsProducts_WhenIdNotTrue()
        {
            //Arrange
            var product = A.Fake<ICollection<ProductDTO>>();
            var productList = A.Fake<IEnumerable<ProductDTO>>();
            A.CallTo(() => _mapper.Map<IEnumerable<ProductDTO>>(product)).Returns(productList);
            var controller = new PProduct(_productContext, _mapper);

            //Act
            var result = await controller.GetProductId(-1);

            //Assert
            //result.Should().BeNull();                                       // To debug when test failed.
            Assert.IsType<ActionResult<Response>>(result);
            Assert.False(result.Value.Success);

        }

        [Fact]
        public async Task GetProductWithID_ReturnsProducts_WhenIdTrue()
        {
            //Arrange
            var product = A.Fake<ICollection<ProductDTO>>();
            var productList = A.Fake<IEnumerable<ProductDTO>>();
            A.CallTo(() => _mapper.Map<IEnumerable<ProductDTO>>(product)).Returns(productList);
            var controller = new PProduct(_productContext, _mapper);

            //Act
            var result = await controller.GetProductId(1);

            //Assert
            //result.Should().BeNull();                                       // To debug when test failed.
            Assert.IsType<ActionResult<Response>>(result);
            Assert.True(result.Value.Success);
            Assert.Equal(1,result.Value.data.Total);

        }

        [Fact]
        public async Task GetProductFindPage_FiltersByName()
        {
            //Arrange
            var product = A.Fake<ICollection<ProductDTO>>();
            var productList = A.Fake<IEnumerable<ProductDTO>>();
            A.CallTo(() => _mapper.Map<IEnumerable<ProductDTO>>(product)).Returns(productList);
            var controller = new PProduct(_productContext, _mapper);

            //Act
            var result = await controller.GetProductFindPage("Product", "Name", 1, 10);

            //Assert
            //result.Should().BeNull();                                       // To debug when test failed.
            Assert.IsType<ActionResult<Response>>(result);
            Assert.True(result.Value.Success);
        }

        [Fact]
        public async Task DeleteProduct_Successfully()
        {
            //Arrange
            var product = A.Fake<ICollection<Product>>();
            var productList = A.Fake<Product>();
            A.CallTo(() => _mapper.Map<Product>(product)).Returns(productList);
            var controller = new PProduct(_productContext, _mapper);

            //Act
            var result = await controller.DeleteProduct(1);

            //Assert
            result.Should().BeNull();                                       // To debug when test failed.
            Assert.IsType<ActionResult<Response>>(result);
            Assert.True(result.Value.Success);
        }
        [Fact]
        public async Task DeleteProduct_InvalidID_Failed()
        {
            //Arrange
            var product = A.Fake<ICollection<ProductDTO>>();
            var productList = A.Fake<IEnumerable<ProductDTO>>();
            A.CallTo(() => _mapper.Map<IEnumerable<ProductDTO>>(product)).Returns(productList);
            var controller = new PProduct(_productContext, _mapper);

            //Act
            var result = await controller.DeleteProduct(-1);

            //Assert
            //result.Should().BeNull();                                       // To debug when test failed.
            Assert.False(result.Value.Success);
            Assert.Equal("Invalid ID!", result.Value.Message);
        }

    }
}

