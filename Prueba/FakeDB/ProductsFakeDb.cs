using Prueba.Modelos;
using System.Collections.Generic;

namespace Prueba.FakeDB
{
    public class ProductsFakeDb
    {
        private List<Product> _fakeProducts = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "Zapatos",
                Price = 100
            },
            new Product
            {
                Id = 2,
                Name = "Camisa",
                Price = 100
            },
            new Product
            {
                Id = 3,
                Name = "Pantalon",
                Price = 100
            },
            new Product
            {
                Id = 4,
                Name = "Reloj",
                Price = 100
            },
            new Product
            {
                Id = 5,
                Name = "Correa",
                Price = 100
            }
        };
        public List<Product> FakeProducts
        {
            get
            {
                return _fakeProducts;
            }
            set
            {
                _fakeProducts = FakeProducts;
            }
        }
    }
}
