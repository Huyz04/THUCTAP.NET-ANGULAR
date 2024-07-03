using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using FluentAssertions;
using INTERN.Controllers;
using INTERN.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using FakeItEasy;
using INTERN.Providers;
using INTERN.DTO;

namespace INTERN.Tests.Controllers
{
    public class TestProductController
    {
        private readonly ProductContext _productContext;
        private readonly IMapper _mapper;
        //private readonly PProduct _pProduct;
        public TestProductController()
        {
            var options = new DbContextOptionsBuilder<ProductContext>()
                .UseInMemoryDatabase(databaseName: "ProductDatabase")
                .Options;

            _productContext = new ProductContext(options);
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
            result.Should().NotBeNull();
            var okResult = Assert.IsType<ActionResult<Response>>(result);


        }
        /*
        [Fact]
        public async Task GetProductFindPage_ReturnsProducts_WhenNoFilterApplied()
        {
            
        }

        [Fact]
        public async Task GetProductFindPage_FiltersByName()
        {
            
        }

        [Fact]
        public async Task GetProductFindPage_FiltersByType()
        {
        }

        [Fact]
        public async Task GetProductFindPage_ReturnsPagedResult()
        {
            
        }
        */
    }
}

