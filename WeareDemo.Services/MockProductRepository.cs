using System;
using System.Collections.Generic;
using System.Text;
using WeareDemo.Models;
using System.Linq;

namespace WeareDemo.Services
{
    public class MockProductRepository : IProductRepository
    {
        private List<Product> _productList;

        public MockProductRepository()
        {
            _productList = new List<Product>()
          {
              new Product() {Id = 1, Name="Sunscreen",
              Cost = 10.5, PhotoPath = "sunscreen.png", Type = ProductType.Product },
                new Product() {Id = 2, Name="Facial cream",
              Cost = 18, PhotoPath = "facialCream.png", Type = ProductType.Product },
                  new Product() {Id = 3, Name="Body cream",
              Cost = 22, PhotoPath = "bodyCream.png", Type = ProductType.Product },
                    new Product() {Id = 4, Name="Pilling",
              Cost = 10, PhotoPath = "pilling.png", Type = ProductType.Service },
          };
        }

        public Product Add(Product newProduct)
        {
            newProduct.Id = _productList.Max(p => p.Id) + 1;
            _productList.Add(newProduct);
            return newProduct;
        }

        public Product Delete(int id)
        {
            Product productToDelete = _productList.FirstOrDefault(p => p.Id == id);
            if(productToDelete != null)
            {
                _productList.Remove(productToDelete);
            }

            return productToDelete;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _productList;
        }

        public Product GetProduct(int id)
        {
            return _productList.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<TypeHeadCount> ProductCountByType(ProductType? type)
        {
            IEnumerable<Product> query = _productList;
            if(type.HasValue)
            {
                query = query.Where(p => p.Type == type);
            }
            return query.GroupBy(p => p.Type)
                .Select(g => new TypeHeadCount()
                {
                    Type = g.Key,
                    Count = g.Count()
                }).ToList();
        }

        public Product Update(Product updatedProduct)
        {
            Product product = _productList.FirstOrDefault(p => p.Id == updatedProduct.Id);

            if(product != null)
            {
                product.Name = updatedProduct.Name;
                product.Cost = updatedProduct.Cost;
                product.Type = updatedProduct.Type;
                product.PhotoPath = updatedProduct.PhotoPath;
            }

            return product;
        }
    }
}
