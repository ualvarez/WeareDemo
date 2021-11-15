using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeareDemo.Models;
using WeareDemo.Services;

namespace WeareDemo.Pages.Products
{
    public class DeleteModel : PageModel
    {
        private readonly IProductRepository productRepository;

        public DeleteModel(IProductRepository productRepository )
        {
            this.productRepository = productRepository;
        }

        [BindProperty]
        public Product Product { get; set; }
        public IActionResult OnGet(int id)
        {
           Product = productRepository.GetProduct(id);

            if(Product == null)
            {
                return RedirectToPage("/NotFound");
            }

            var result = Page();

            return result;
        }

        public IActionResult OnPost()
        {
            Product deletedProduct = productRepository.Delete(Product.Id);

            if(deletedProduct == null)
            {
                return RedirectToPage("/NotFound");
            }

            return RedirectToPage("Index");
        }
    }
}
