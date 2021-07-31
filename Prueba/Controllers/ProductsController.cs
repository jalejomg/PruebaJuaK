using Microsoft.AspNetCore.Mvc;
using Prueba.FakeDB;
using Prueba.Modelos;
using Prueba.Validations;
using System.Collections.Generic;
using System.Linq;

namespace Prueba.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsFakeDb _fakeDb;
        private readonly ProductValidator _productValidator;

        public ProductsController(
            ProductsFakeDb fakeDb,
            ProductValidator productValidator)
        {
            _fakeDb = fakeDb;
            _productValidator = productValidator;
        }
        [HttpGet("/")]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            return Ok(_fakeDb.FakeProducts);
        }

        [HttpGet("/{productId:int}")]
        public ActionResult<Product> GetById(int productId)
        {
            if (productId <= 0)
            {
                return BadRequest("El Id es un campo obligatorio");
            }

            var product = _fakeDb.FakeProducts.Where(p => p.Id == productId).FirstOrDefault();

            if (product == null)
            {
                return NotFound("El producto no existe");
            }

            return Ok(product);
        }

        [HttpDelete("/{productId:int}")]
        public ActionResult Delete(int productId)
        {
            if (productId <= 0)
            {
                return BadRequest("El Id es un campo obligatorio");
            }

            var productExists = _fakeDb.FakeProducts.Any(p => p.Id == productId);

            if (!productExists)
            {
                return NotFound("El producto no existe");
            }
            else
            {
                var Products = _fakeDb.FakeProducts.FindIndex(p => p.Id == productId);
                _fakeDb.FakeProducts.RemoveAt(Products);
            }

            return Ok();
        }

        [HttpPut("/{productId:int}")]
        public ActionResult Update(int productId, [FromBody] Product product)
        {
            var validation = _productValidator.Validate(product);

            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }

            if (productId <= 0)
            {
                return BadRequest("El Id es un campo obligatorio");
            }

            var productExists = _fakeDb.FakeProducts.Any(p => p.Id == productId);

            if (!productExists)
            {
                return NotFound("El producto no existe");
            }
            else
            {
                var Products = _fakeDb.FakeProducts.FindIndex(p => p.Id == productId);
                _fakeDb.FakeProducts.ElementAt(Products).Name = product.Name;
                _fakeDb.FakeProducts.ElementAt(Products).Price = product.Price;
            }

            return Ok();
        }

        [HttpPost]
        public ActionResult<int> Save([FromBody] Product product)
        {
            var validation = _productValidator.Validate(product);

            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }

            var lastId = _fakeDb.FakeProducts.ElementAt(_fakeDb.FakeProducts.Count -1).Id;

            product.Id = ++lastId;

            _fakeDb.FakeProducts.Add(product);

            return Ok(_fakeDb.FakeProducts.Last().Id);
        }

    }
}
