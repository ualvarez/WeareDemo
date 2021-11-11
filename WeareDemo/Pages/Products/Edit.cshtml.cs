using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeareDemo.Models;
using WeareDemo.Services;

namespace WeareDemo.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly IProductRepository productRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        [BindProperty]
        public Product Product { get; set; }

        public string PhotoPath { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }

        public EditModel(IProductRepository productRepository, IWebHostEnvironment webHostEnvironment)
        {
            this.productRepository = productRepository;
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult OnGet(int? id)
        {
            if(id.HasValue)
            {
                this.Product = productRepository.GetProduct(id.Value);
            }
            else
            {
                this.Product = new Product();
            }
           
           
            if (this.Product == null)
            {
                return RedirectToPage("/Notfound");
            }

            this.PhotoPath = "~/images/" + (this.Product.PhotoPath ?? "noImage.png");

            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid) 
            {
                if (Photo != null)
                {
                    if (Product.PhotoPath != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", Product.PhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    Product.PhotoPath = ProcessUploadedFile();
                }
                if(Product.Id > 0)
                {
                    Product = productRepository.Update(Product);
                }
                else
                {
                    Product = productRepository.Add(Product);
                }
               
                return RedirectToPage("Index");
            }

            return Page();
        }

        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;

            if(Photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using(var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
