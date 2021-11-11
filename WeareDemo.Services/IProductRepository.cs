using System;
using System.Collections.Generic;
using WeareDemo.Models;

namespace WeareDemo.Services
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
       Product GetProduct(int id);
        Product Update(Product updatedProduct);
        Product Add(Product newProduct);
        Product Delete(int id);

        IEnumerable<TypeHeadCount> ProductCountByType(ProductType? type);
    }
}
