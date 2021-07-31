using Microsoft.AspNetCore.Mvc;
using Prueba.Controllers;
using Prueba.FakeDB;
using Prueba.Modelos;
using Prueba.Validations;
using Xunit;

namespace Prueba.Tests
{
    public class ProductControllerTests
    {
        private ProductsFakeDb _fakeDb;
        private ProductValidator _productValidator;
        private ProductsController _controller;

        public ProductControllerTests()
        {
            _fakeDb = new ProductsFakeDb();
            _productValidator = new ProductValidator();
            _controller = new ProductsController(_fakeDb, _productValidator);
        }

        [Fact]
        public void Should_return_new_product_id()
        {
            //Arrange
            var product = new Product
            {
                Name = "Name",
                Price = 125
            };

            //Act
            var result = _controller.Save(product);
            var convertedRersult = result.Result as OkObjectResult;

            //Assert
            Assert.IsType<int>(convertedRersult.Value);
        }

        [Fact]
        public void Should_return_BadRequest_for_validation_of_model_failure_name_empty()
        {
            //Arrange
            var product = new Product
            {
                Name = "",
                Price = 125
            };

            //Act
            var result = _controller.Save(product);

            //Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void Should_return_BadRequest_for_validation_of_model_failure_price_zero()
        {
            //Arrange
            var product = new Product
            {
                Name = "Name",
                Price = 0
            };

            //Act
            var result = _controller.Save(product);

            //Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);
        }
    }
}
