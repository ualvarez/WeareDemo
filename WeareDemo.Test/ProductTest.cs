using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Text.RegularExpressions;
using WeareDemo.Models;
using WeareDemo.Pages.Products;
using WeareDemo.Services;

namespace WeareDemo.Test
{
    [TestClass]
    public class ProductTest
    {
        private readonly IProductRepository repository = new MockProductRepository();
        private static WebApplicationFactory<Startup> _factory;

        public ProductTest()
        {
           
        }

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            _factory = new WebApplicationFactory<Startup>();
        }

        [TestMethod]
        [TestCategory("WEB-Product")]
        public void TestDeleteModel_OnGet_ReturnIActionResult()
        {
            var productToDelete = repository.GetAllProducts().ToList().FirstOrDefault();
            
            DeleteModel page = new DeleteModel(repository);

            var result  = page.OnGet(productToDelete.Id);


            Assert.IsInstanceOfType(result, typeof(IActionResult));

            System.Console.WriteLine(result.ToString());
        }


        [TestMethod]
        [TestCategory("WEB-Product")]
        public void TestDeleteModel_OnPost_ReturnIActionResult()
        {
           
                // Arrange
                var list = repository.GetAllProducts();
              
                var recId = 1;
                var expected =
                    list.Where(x => x.Id != recId).ToList();

                // Act
                DeleteModel page = new DeleteModel(repository) { Product = repository.GetProduct(recId)};
            page.OnGet(recId);
            var result = page.OnPost();

                // Assert
                var actual = repository.GetAllProducts();
                CollectionAssert.AreEqual(
                    expected,
                    actual.ToList(), new ProductComparer());
            
        }
    }
}
