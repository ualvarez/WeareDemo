using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeareDemo.Models;
using WeareDemo.Services;

namespace WeareDemo.ViewComponents
{
    public class HeadCountViewComponent : ViewComponent
    {
        private readonly IProductRepository productRepository;

        public HeadCountViewComponent(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public IViewComponentResult Invoke(ProductType? type = null)
        {
            var result = this.productRepository.ProductCountByType(type);

            return View(result);
        }
    }
}
