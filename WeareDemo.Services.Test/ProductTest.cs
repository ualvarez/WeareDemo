using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeareDemo.Models;
using System.Linq;
using System.Collections.Generic;

namespace WeareDemo.Services.Test
{
    [TestClass]
    public class ProductTest
    {
        private readonly IProductRepository repository = new DBProductRepository();
        

        [TestMethod]
        public void Test_GetAllProducts_ReturnCollectionOfProducts()
        {
            //Arrange            

           
            //Act
            var expected = repository.GetAllProducts();

            //Assert
            Assert.IsNotNull(expected);
            Assert.IsInstanceOfType(expected, typeof(IEnumerable<Product>));
        }

        [TestMethod]
        public void Test_GetProduct_ReturnProduct()
        {
            //Arrange            

            

            //Act
            var expected = repository.GetProduct(repository.GetAllProducts().FirstOrDefault().Id);

            //Assert
            Assert.IsNotNull(expected);
            Assert.IsInstanceOfType(expected, typeof(Product));
        }



        [TestMethod]
        public void Test_Add_Product_ReturnNewProduct()
        {
            //Arrange            
            Product product = new Product()
            {
                Name = "Test",
                Cost = 1.2,
                PhotoPath = "",
                Type = ProductType.Product
            };

           

            //Act
            var expected = repository.Add(product);

            //Assert
            Assert.IsNotNull(expected);
            Assert.IsInstanceOfType(expected, typeof(Product));
            Assert.IsNotNull(expected.Id);
           
        }

        [TestMethod]
        public void Test_Update_Product_ReturnUpdatedProduct()
        {
            //Arrange            
          
          

            var product = repository.GetAllProducts().FirstOrDefault();

            product.Name = "Test Modified";

            //Act
            var expected = repository.Update(product);

            //Assert
            Assert.IsNotNull(expected);
            Assert.IsInstanceOfType(expected, typeof(Product));
            Assert.AreEqual(product, expected);
        }

        [TestMethod]
        public void Test_ProductCountByType_ReturnCollectionOfTypeHeadCount()
        {
            //Arrange            

           

            //Act
            var expected = repository.ProductCountByType(ProductType.Product);

            //Assert
            Assert.IsNotNull(expected);
            Assert.IsInstanceOfType(expected, typeof(IEnumerable<TypeHeadCount>));
        }
    }
}
